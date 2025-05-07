using PeopleHub.Domain.Enums;
using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.IndividualPerson;

public class AddIndividualPersonRequestDto
{
    private string _cpf = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string Cpf
    {
        get => _cpf;
        set => _cpf = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; } = string.Empty;
}