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

    public override string ToString()
    {
        return $"{Value.Substring(0, 3)}.{Value.Substring(3, 3)}.{Value.Substring(6, 3)}-{Value.Substring(9, 2)}";
    }

    public static implicit operator string(Cpf? cpf) => cpf?.Value ?? string.Empty;

    /// <summary>
    /// Method to check if the CPF is null or empty
    /// </summary>
    public bool IsEmpty() => string.IsNullOrWhiteSpace(Value);
}
