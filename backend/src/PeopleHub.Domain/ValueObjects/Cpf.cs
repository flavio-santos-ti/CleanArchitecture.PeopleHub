using System.Text.RegularExpressions;

namespace PeopleHub.Domain.ValueObjects;

public class Cpf
{
    public string Value { get; private set; }

    private static readonly Regex OnlyNumbers = new Regex(@"[^\d]");

    public Cpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF is required.");

        var cleanedCpf = OnlyNumbers.Replace(cpf, "");

        if (cleanedCpf.Length != 11)
            throw new ArgumentException("CPF must have 11 digits.");

        Value = cleanedCpf;
    }

    /// <summary>
    /// Removes any non-numeric characters from the CPF.
    /// </summary>
    public static string Clean(string cpf)
    {
        return OnlyNumbers.Replace(cpf, "");
    }

    /// <summary>
    /// Checks if a CPF is valid without throwing excpetions.
    /// Returns 'null' if valid, or an error message if invalid.
    /// </summary>
    public static string? Validate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return "CPF is required.";

        var cleanedCpf = OnlyNumbers.Replace(cpf, "");

        if (cleanedCpf.Length != 11)
            return "CPF must have 11 digits.";

        return null; // Valid CPF 
    }

    public static implicit operator string(Cpf? cpf) => cpf?.Value ?? string.Empty;

    /// <summary>
    /// Method to check if the CPF is null or empty
    /// </summary>
    public bool IsEmpty() => string.IsNullOrWhiteSpace(Value);
}
