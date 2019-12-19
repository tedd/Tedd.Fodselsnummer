# Tedd.Fodselsnummer
C#/.Net parser for fødselsnummer / Norwegian national identity number

# Example
```csharp
var result = FodselsnummerValidator.Validate("19121950041");
if (!result.Success)
{
    Console.WriteLine(result.ErrorMessage);
}
else
{
    Console.WriteLine($"Fødselsnummer type: {result.Type}");
    Console.WriteLine($"Kjønn: {result.Gender}");
    Console.WriteLine($"Fødselsdato: {result.Birthday}");
}
```