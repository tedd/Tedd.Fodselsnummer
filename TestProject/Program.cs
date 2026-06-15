using System;
using Tedd.Fodselsnummer;

var result = FodselsnummerValidator.Validate("19121950041");

if (result is { Success: true })
{
    Console.WriteLine($"Fødselsnummer type: {result.Type}");
    Console.WriteLine($"Kjønn: {result.Gender}");
    Console.WriteLine($"Fødselsdato: {result.Birthday:yyyy-MM-dd}");
}
else
{
    Console.WriteLine($"Validation failed: {result.ErrorMessage}");
}
