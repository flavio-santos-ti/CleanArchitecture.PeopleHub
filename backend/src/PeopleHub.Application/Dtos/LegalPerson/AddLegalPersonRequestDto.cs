using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.LegalPerson;

public class AddLegalPersonRequestDto
{
    private string _cnpj = string.Empty;
    private string _cpf = string.Empty;

    public string LegalName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string Cnpj
    {
        get => _cnpj;
        set => _cnpj = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
    public string StateRegistration { get; set; } = string.Empty;
    public string MunicipalRegistration { get; set; } = string.Empty;
    public string LegalRepresentativeName { get; set; } = string.Empty;
    public string LegalRepresentativeCpf
    {
        get => _cpf;
        set => _cpf = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
}