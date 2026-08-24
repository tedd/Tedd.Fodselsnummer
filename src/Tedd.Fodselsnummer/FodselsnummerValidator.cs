using System;
using System.Globalization;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace Tedd.Fodselsnummer;

public static class FodselsnummerValidator
{
    private struct IndividualNumberControlRange
    {
        public int From;
        public int To;
        public int FromYear;
        public int ToYear;
    }

    private static readonly IndividualNumberControlRange[] IndividualControlRange = new[] {
            new IndividualNumberControlRange() { From = 0,   To = 499, FromYear = 1900, ToYear = 1999 },
            new IndividualNumberControlRange() { From = 500, To = 749, FromYear = 1854, ToYear = 1899 },
            new IndividualNumberControlRange() { From = 500, To = 999, FromYear = 2000, ToYear = 2039 },
            new IndividualNumberControlRange() { From = 900, To = 999, FromYear = 1940, ToYear = 1999 },
        };

    // Time complexity: O(1)
    // Space complexity: O(1)
    public static FodselsnummerResult Validate(long number)
    {
        if (number < 1000000000L || number > 99999999999L)
            return FodselsnummerResult.FromError(1);

        long n = number;
        int n10 = (int)(n % 10); n /= 10;
        int n9 = (int)(n % 10); n /= 10;
        int n8 = (int)(n % 10); n /= 10;
        int n7 = (int)(n % 10); n /= 10;
        int n6 = (int)(n % 10); n /= 10;
        int n5 = (int)(n % 10); n /= 10;
        int n4 = (int)(n % 10); n /= 10;
        int n3 = (int)(n % 10); n /= 10;
        int n2 = (int)(n % 10); n /= 10;
        int n1 = (int)(n % 10); n /= 10;
        int n0 = (int)n;

        return ValidateInternal(number, n0, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10);
    }

    // Time complexity: O(1)
    // Space complexity: O(1)
    public static FodselsnummerResult Validate(string number)
    {
        // Is it the correct length?
        if (number == null || number.Length != 11)
            return FodselsnummerResult.FromError(1);

        // Parse and validate digits manually to eliminate allocations
        int n0 = number[0] - '0';
        int n1 = number[1] - '0';
        int n2 = number[2] - '0';
        int n3 = number[3] - '0';
        int n4 = number[4] - '0';
        int n5 = number[5] - '0';
        int n6 = number[6] - '0';
        int n7 = number[7] - '0';
        int n8 = number[8] - '0';
        int n9 = number[9] - '0';
        int n10 = number[10] - '0';

        if (n0 < 0 || n0 > 9 || n1 < 0 || n1 > 9 || n2 < 0 || n2 > 9 || n3 < 0 || n3 > 9 ||
            n4 < 0 || n4 > 9 || n5 < 0 || n5 > 9 || n6 < 0 || n6 > 9 || n7 < 0 || n7 > 9 ||
            n8 < 0 || n8 > 9 || n9 < 0 || n9 > 9 || n10 < 0 || n10 > 9)
        {
            return FodselsnummerResult.FromError(2);
        }

        long fodselsnummer = long.Parse(number, CultureInfo.InvariantCulture);

        return ValidateInternal(fodselsnummer, n0, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10);
    }

    private static FodselsnummerResult ValidateInternal(long fodselsnummer, int n0, int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10)
    {
        int day = n0 * 10 + n1;
        int month = n2 * 10 + n3;
        int year = n4 * 10 + n5;
        int individual = n6 * 100 + n7 * 10 + n8;
        int gender = n8;
        int checksum = n9 * 10 + n10;

        var result = new FodselsnummerResult()
        {
            Fodselsnummer = fodselsnummer,
            Individnummer = individual,
            Kontrollsifre = checksum
        };

        // Compensate for FH-numbers
        if (day >= 80)
        {
            result.Type |= FodselsnummerType.FH;
            day = 1;
        }
        else
        {
            //
            // Only if not FH-number
            //

            // Compensate for D-numbers
            if (day > 40)
            {
                result.Type |= FodselsnummerType.D;
                day -= 40;
            }
            // Compensate for H-numbers
            if (month > 40)
            {
                result.Type |= FodselsnummerType.H;
                month -= 40;
            }

            // Determine gender
            result.Gender = gender % 2 != 0 ? Gender.Male : Gender.Female;

            int fullYear = 0;

            bool rangeFound = false;
            foreach (var range in IndividualControlRange)
            {
                if (individual >= range.From && individual <= range.To)
                {
                    rangeFound = true;
                    // Note that if there is a change to range so it crosses centuries then we need more checking here - or else the next check will fail
                    var fYear = (range.FromYear / 100) * 100 + year;

                    // Check that we are within allowed range
                    if (fYear >= range.FromYear && fYear <= range.ToYear)
                    {
                        fullYear = fYear;
                        break;
                    }
                }
            }

            if (!rangeFound)
                return FodselsnummerResult.FromError(3);

            if (fullYear == 0)
                return FodselsnummerResult.FromError(4);

            try
            {
                result.Birthday = new DateTime(fullYear, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                return FodselsnummerResult.FromError(5);
            }

        } // End of "only if not FH"-check

        // And finally calculate and verify the two checksum digits at the end of the number

        // Calculate checksum number 1
        int k1 = 11 - (3 * n0 + 7 * n1 + 6 * n2 + 1 * n3 + 8 * n4 + 9 * n5 + 4 * n6 + 5 * n7 + 2 * n8) % 11;
        if (k1 == 11) k1 = 0;

        if (k1 == 10 || k1 != n9)
            return FodselsnummerResult.FromError(6);

        // Calculate checksum number 2
        int k2 = 11 - (5 * n0 + 4 * n1 + 3 * n2 + 2 * n3 + 7 * n4 + 6 * n5 + 5 * n6 + 4 * n7 + 3 * n8 + 2 * k1) % 11;
        if (k2 == 11) k2 = 0;

        if (k2 == 10 || k2 != n10)
            return FodselsnummerResult.FromError(7);

        result.Success = true;
        return result;
    }
}
