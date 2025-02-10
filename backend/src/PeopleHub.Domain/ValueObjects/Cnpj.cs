using System.Text.RegularExpressions;

namespace PeopleHub.Domain.ValueObjects;

public class Cnpj
{
    public string Value { get; private set; }

    private static readonly Regex OnlyNumbers = new Regex(@"[^\d]");

    public Cnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ is required.");

        var cleanedCnpj = OnlyNumbers.Replace(cnpj, "");

        if (cleanedCnpj.Length != 14)
            throw new ArgumentException("CNPJ must have 14 digits.");

        Value = cleanedCnpj;
    }

    public override string ToString()
    {
        return $"{Value.Substring(0, 2)}.{Value.Substring(2, 3)}.{Value.Substring(5, 3)}/{Value.Substring(8, 4)}-{Value.Substring(12, 2)}";
    }

    public static implicit operator string(Cnpj cnpj) => cnpj.Value;

    /// <summary>
    /// Method to check if the CNPJ is null or empty
    /// </summary>
    public bool IsEmpty() => string.IsNullOrWhiteSpace(Value);
}
