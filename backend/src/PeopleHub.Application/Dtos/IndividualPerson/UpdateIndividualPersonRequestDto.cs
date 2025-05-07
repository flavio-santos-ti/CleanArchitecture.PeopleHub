using PeopleHub.Domain.Enums;
using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.IndividualPerson;

public class UpdateIndividualPersonRequestDto
{
    private string _cpf = string.Empty;

    public string Cpf
    {
        get => _cpf;
        set => _cpf = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
    public DateTime BirthDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
