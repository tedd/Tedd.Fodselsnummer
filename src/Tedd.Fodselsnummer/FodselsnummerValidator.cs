using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

[assembly: CLSCompliant(true)]
namespace Tedd.Fodselsnummer;

public static class FodselsnummerValidator
{
    // Implementation of Norwegian personal number verification based on specifications outlined on https://no.wikipedia.org/wiki/F%C3%B8dselsnummer
    // Version 1: 2016-12-07 Tedd Hansen

    // Time complexity: O(1)
    // Space complexity: O(1)
    public static FodselsnummerResult Validate(long number)
    {
        if (number >= 10000000000L && number <= 99999999999L)
        {
            return Validate(number.ToString(CultureInfo.InvariantCulture));
        }
        else if (number >= 0 && number <= 9999999999L)
        {
            return Validate(number.ToString("D11", CultureInfo.InvariantCulture));
        }
        return FodselsnummerResult.FromError(1);
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

        int day = n0 * 10 + n1;
        int month = n2 * 10 + n3;
        int year = n4 * 10 + n5;
        int individual = n6 * 100 + n7 * 10 + n8;
        int gender = n8;
        int checksum = n9 * 10 + n10;

        long parsedNumber = n0 * 10000000000L + n1 * 1000000000L + n2 * 100000000L + n3 * 10000000L + n4 * 1000000L + n5 * 100000L + n6 * 10000L + n7 * 1000L + n8 * 100L + n9 * 10L + n10;

        var result = new FodselsnummerResult()
        {
            Fodselsnummer = parsedNumber,
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
            if (individual <= 499)
            {
                rangeFound = true;
                int y = 1900 + year;
                if (y <= 1999) fullYear = y;
            }
            else if (individual >= 500 && individual <= 749)
            {
                rangeFound = true;
                int y = 1800 + year;
                if (y >= 1854 && y <= 1899) fullYear = y;
                else
                {
                    y = 2000 + year;
                    if (y >= 2000 && y <= 2039) fullYear = y;
                }
            }
            else if (individual >= 750 && individual <= 899)
            {
                rangeFound = true;
                int y = 2000 + year;
                if (y >= 2000 && y <= 2039) fullYear = y;
            }
            else if (individual >= 900 && individual <= 999)
            {
                rangeFound = true;
                int y = 1900 + year;
                if (y >= 1940 && y <= 1999) fullYear = y;
                else
                {
                    y = 2000 + year;
                    if (y >= 2000 && y <= 2039) fullYear = y;
                }
            }

            if (!rangeFound)
                return FodselsnummerResult.FromError(3);

            if (fullYear == 0)
                return FodselsnummerResult.FromError(4);

            if (fullYear >= 1 && fullYear <= 9999 && month >= 1 && month <= 12 && day >= 1 && day <= DateTime.DaysInMonth(fullYear, month))
            {
                result.Birthday = new DateTime(fullYear, month, day);
            }
            else
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