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

    private static Regex PersonalNumberRegex = new Regex(@"^(?<birthdate>(?<day>\d\d)(?<month>\d\d)(?<year>\d\d))(?<individual>\d\d(?<gender>\d))(?<checksum>\d\d)$");
    public static FodselsnummerResult Validate(long number)
    {
        return Validate(number.ToString(CultureInfo.InvariantCulture));
    }

    public static FodselsnummerResult Validate(string number)
    {
        // Is it the correct length?
        if (number == null || number.Length != 11)
            return FodselsnummerResult.FromError(1);

        // Extract what we need
        var match = PersonalNumberRegex.Match(number);

        // Regex is only looking for 11 numbers, so we can assume any error is because there are non-numbers
        if (!match.Success)
            return FodselsnummerResult.FromError(2);

        // Parse the numbers
        var day = int.Parse(match.Groups["day"].Value, CultureInfo.InvariantCulture);
        var month = int.Parse(match.Groups["month"].Value, CultureInfo.InvariantCulture);
        var year = int.Parse(match.Groups["year"].Value, CultureInfo.InvariantCulture);
        var individual = int.Parse(match.Groups["individual"].Value, CultureInfo.InvariantCulture);
        var gender = int.Parse(match.Groups["gender"].Value, CultureInfo.InvariantCulture);
        var checksum = int.Parse(match.Groups["checksum"].Value, CultureInfo.InvariantCulture);

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
            if (fullYear == 0)
                return FodselsnummerResult.FromError(4);

            // Have .Net verify the date, using ISO 8601 should be universally safe
            var iso8601Birthday = fullYear + "-" + month.ToString("D2", CultureInfo.InvariantCulture) + "-" + day.ToString("D2", CultureInfo.InvariantCulture);
            DateTime dtBirthday;
            if (!DateTime.TryParse(iso8601Birthday, out dtBirthday))
                return FodselsnummerResult.FromError(5);
            result.Birthday = dtBirthday;

        } // End of "only if not FH"-check

        // And finally calculate and verify the two checksum digits at the end of the number
        // Convert personal number string to int array
        var n = number.Select(c => int.Parse(c.ToString(), CultureInfo.InvariantCulture)).ToArray();

        // Calculate checksum number 1
        int k1 = 11 - (3 * n[0] + 7 * n[1] + 6 * n[2] + 1 * n[3] + 8 * n[4] + 9 * n[5] + 4 * n[6] + 5 * n[7] + 2 * n[8]) % 11;
        if (k1 == 11) k1 = 0;

        if (k1 == 10 || k1 != n[9])
            return FodselsnummerResult.FromError(6);

        // Calculate checksum number 2
        int k2 = 11 - (5 * n[0] + 4 * n[1] + 3 * n[2] + 2 * n[3] + 7 * n[4] + 6 * n[5] + 5 * n[6] + 4 * n[7] + 3 * n[8] + 2 * k1) % 11;
        if (k2 == 11) k2 = 0;

        if (k2 == 10 || k2 != n[10])
            return FodselsnummerResult.FromError(7);

        result.Success = true;
        return result;
    }
}