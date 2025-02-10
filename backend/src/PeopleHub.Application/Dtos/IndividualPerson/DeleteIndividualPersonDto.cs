using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.IndividualPerson;

public class DeleteIndividualPersonDto
{
    private string _cpf = string.Empty;

    public string Cpf
    {
        get => _cpf;
        set => _cpf = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
}
