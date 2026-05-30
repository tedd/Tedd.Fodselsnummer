using System;

[assembly: CLSCompliant(true)]
namespace Tedd.Fodselsnummer;

/// <summary>
/// Time Complexity: O(1) - The validation process executes in constant time as the input length is strictly bounded to 11 characters.
/// Space Complexity: O(1) - Validation uses only bounded local state and avoids intermediate heap allocations such as Regex, LINQ, or parsing-related temporaries during the validation logic.
/// </summary>
public static class FodselsnummerValidator
{
    // Implementation of Norwegian personal number verification based on specifications outlined on https://no.wikipedia.org/wiki/F%C3%B8dselsnummer
    // Version 1: 2016-12-07 Tedd Hansen

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

    public static FodselsnummerResult Validate(long number)
    {
        return Validate(number.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    public static FodselsnummerResult Validate(string number)
    {
        // Is it the correct length?
        if (number == null || number.Length != 11)
            return FodselsnummerResult.FromError(1);

        long fodselsnummer = 0;
        for (int i = 0; i < 11; i++)
        {
            char c = number[i];
            if (c < '0' || c > '9')
                return FodselsnummerResult.FromError(2);
            fodselsnummer = fodselsnummer * 10 + (c - '0');
        }

        // Parse the numbers
        var day = (number[0] - '0') * 10 + (number[1] - '0');
        var month = (number[2] - '0') * 10 + (number[3] - '0');
        var year = (number[4] - '0') * 10 + (number[5] - '0');
        var individual = (number[6] - '0') * 100 + (number[7] - '0') * 10 + (number[8] - '0');
        var gender = number[8] - '0';
        var checksum = (number[9] - '0') * 10 + (number[10] - '0');

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
            // Only if not FH-number

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

            for (int i = 0; i < IndividualControlRange.Length; i++)
            {
                ref var range = ref IndividualControlRange[i];
                if (individual >= range.From && individual <= range.To)
                {
                    // Get full year based on the range
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

            if (fullYear == 0)
                return FodselsnummerResult.FromError(4);

            // Have .Net verify the date
            if (fullYear < 1 || fullYear > 9999 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(fullYear, month))
                return FodselsnummerResult.FromError(5);

            result.Birthday = new DateTime(fullYear, month, day);

        } // End of "only if not FH"-check

        // Convert personal number string to int array equivalent for checksum calculations
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
