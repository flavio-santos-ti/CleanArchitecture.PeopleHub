using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.LegalPerson;

public class DeleteLegalPersonDto
{
    private string _cnpj = string.Empty;

    public string Cnpj
    {
        get => _cnpj;
        set => _cnpj = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
}
