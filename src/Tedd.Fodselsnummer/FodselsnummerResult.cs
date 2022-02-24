using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tedd.Fodselsnummer;

public class FodselsnummerResult
{
#pragma warning disable CA2211 // Non-constant fields should not be visible
    public static string[] ErrorStrings = new string[]
#pragma warning restore CA2211 // Non-constant fields should not be visible
    {
            "",
            "Feil lengde. Personnummer er 11 siffer.",
            "Personummer skal kun inneholde siffer.",
            "Feil i personnummer: Ugyldig individrekke.",
            "Feil i personnummer: Ugyldig årsrekke.",
            "Feil i personnummer: Ugyldig dato.",
            "Feil i personnummer: Ugyldig kontrollsiffer 1.",
            "Feil i personnummer: Ugyldig kontrollsiffer 2."
    };

    public bool Success { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public Gender Gender { get; set; }
    public FodselsnummerType Type { get; set; }
    public DateTime? Birthday { get; set; }
    public long Fodselsnummer { get; set; }
    public int Individnummer { get; set; }
    public int Kontrollsifre { get; set; }

    public FodselsnummerResult()
    {
    }

    internal FodselsnummerResult(int errorCode, string errorMessage)
    {
        Success = false;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    internal static FodselsnummerResult FromError(int errorCode)
    {
        return new FodselsnummerResult(errorCode, ErrorStrings[errorCode]);
    }

    public override string ToString()
    {
        if (Success)
            return $"Success: {Success}"
                + $", Type: {Type}"
                + $", Gender: {Gender}"
                + $", Birthday: {(Birthday.HasValue ? Birthday.Value.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture) : "NA")}"
                ;
        else
            return $"Success: {Success}, ErrorMessage: {ErrorMessage ?? "N/A"}";
    }
}
