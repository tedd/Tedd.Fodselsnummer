using System;
using System.Collections.Generic;
using System.Text;

namespace Tedd.Fodselsnummer
{
    public class FodselsnummerResult
    {
        public static string[] ErrorStrings = new string[]
        {
            "",
            "Feil lengde. Personnummer er 11 siffer.",
            "Personummer skal kun inneholde siffer.",
            "Feil i personnummer.",
            "Feil i personnummer.",
            "Feil i personnummer.",
            "Feil i personnummer.",
            "Feil i personnummer."
        };

        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
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
        //private FodselsnummerResult(int fodselsnummer, int individnummer, int kontrollsifre, GenderType gender, NumberType type, DateTime? birthday)
        //{
        //    Success = true;
        //    Fodselsnummer = fodselsnummer;
        //    Individnummer = individnummer;
        //    Kontrollsifre = kontrollsifre;
        //    Gender = gender;
        //    Type = type;
        //    Birthday = birthday;
        //}
        internal static FodselsnummerResult FromError(int errorCode)
        {
            return new FodselsnummerResult(errorCode, ErrorStrings[errorCode]);
        }
        //public static FodselsnummerResult From(int fodselsnummer, int individnummer, int kontrollsifre, GenderType gender, NumberType type, DateTime? birthday)
        //{
        //    return new FodselsnummerResult(fodselsnummer, individnummer, kontrollsifre, gender, type, birthday);
        //}
        public override string ToString()
        {
            if (Success)
                return $"Success: {Success}"
                    + $", Type: {Type}"
                    + $", Gender: {Gender}"
                    + $", Birthday: {(Birthday.HasValue ? Birthday.Value.ToString("yyyy'-'MM'-'dd") : "NA")}"
                    ;
            else
                return $"Success: {Success}, ErrorMessage: {ErrorMessage ?? "N/A"}";
        }
    }
}
