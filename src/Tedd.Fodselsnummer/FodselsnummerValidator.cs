using System;
using System.Globalization;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace Tedd.Fodselsnummer;

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
        return Validate(number.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Validates a string representation of a Norwegian identity number.
    /// Big-O Complexity: Time: O(1), Space: O(1) for .NET Standard 2.1+; Space: O(N) for legacy frameworks due to regex and string allocations.
    /// </summary>
    public static FodselsnummerResult Validate(string number)
    {
        // Is it the correct length?
        if (number == null || number.Length != 11)
            return FodselsnummerResult.FromError(1);

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ReadOnlySpan<char> span = number.AsSpan();

        // Fast path: Check if all characters are digits
        for (int i = 0; i < 11; i++)
        {
            if ((uint)(span[i] - '0') > 9)
                return FodselsnummerResult.FromError(2);
        }

        // Parse numbers without string allocations
        int day = (span[0] - '0') * 10 + (span[1] - '0');
        int month = (span[2] - '0') * 10 + (span[3] - '0');
        int year = (span[4] - '0') * 10 + (span[5] - '0');
        int individual = (span[6] - '0') * 100 + (span[7] - '0') * 10 + (span[8] - '0');
        int gender = span[8] - '0';
        int checksum = (span[9] - '0') * 10 + (span[10] - '0');

        int n0 = span[0] - '0';
        int n1 = span[1] - '0';
        int n2 = span[2] - '0';
        int n3 = span[3] - '0';
        int n4 = span[4] - '0';
        int n5 = span[5] - '0';
        int n6 = span[6] - '0';
        int n7 = span[7] - '0';
        int n8 = span[8] - '0';
        int n9 = span[9] - '0';
        int n10 = span[10] - '0';
#else
        // Fallback for older frameworks
        var match = System.Text.RegularExpressions.Regex.Match(number, @"^(?<birthdate>(?<day>\d\d)(?<month>\d\d)(?<year>\d\d))(?<individual>\d\d(?<gender>\d))(?<checksum>\d\d)$");

        if (!match.Success)
            return FodselsnummerResult.FromError(2);

        var day = int.Parse(match.Groups["day"].Value, CultureInfo.InvariantCulture);
        var month = int.Parse(match.Groups["month"].Value, CultureInfo.InvariantCulture);
        var year = int.Parse(match.Groups["year"].Value, CultureInfo.InvariantCulture);
        var individual = int.Parse(match.Groups["individual"].Value, CultureInfo.InvariantCulture);
        var gender = int.Parse(match.Groups["gender"].Value, CultureInfo.InvariantCulture);
        var checksum = int.Parse(match.Groups["checksum"].Value, CultureInfo.InvariantCulture);

        var n = number.Select(c => int.Parse(c.ToString(), CultureInfo.InvariantCulture)).ToArray();
        int n0 = n[0]; int n1 = n[1]; int n2 = n[2]; int n3 = n[3]; int n4 = n[4];
        int n5 = n[5]; int n6 = n[6]; int n7 = n[7]; int n8 = n[8]; int n9 = n[9]; int n10 = n[10];
#endif

        var result = new FodselsnummerResult()
        {
            Fodselsnummer = long.Parse(number, CultureInfo.InvariantCulture),
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

            // Get the range based on the individual range
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
            // Avoid LINQ overhead where possible
            for (int i = 0; i < IndividualControlRange.Length; i++)
            {
                var r = IndividualControlRange[i];
                if (individual >= r.From && individual <= r.To)
                {
                    int rangeCentury = r.FromYear / 100;
                    int fYear = rangeCentury * 100 + year;

                    if (fYear >= r.FromYear && fYear <= r.ToYear)
                    {
                        fullYear = fYear;
                        break;
                    }
                }
            }
#else
            var ranges = IndividualControlRange.Where(r => individual >= r.From && individual <= r.To).ToList();
            if (ranges == null || ranges.Count == 0)
                return FodselsnummerResult.FromError(3);

            foreach (var range in ranges)
            {
                // Get full year based on the range
                // Note that if there is a change to range so it crosses centuries then we need more checking here - or else the next check will fail
                var fYear = int.Parse(range.FromYear.ToString(CultureInfo.InvariantCulture).Substring(0, 2) + year.ToString("D2", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                // Check that we are within allowed range
                if (fYear >= range.FromYear && fYear <= range.ToYear)
                {
                    fullYear = fYear;
                    break;
                }
            }
#endif
            if (fullYear == 0)
                return FodselsnummerResult.FromError(4);

            // Have .Net verify the date
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
