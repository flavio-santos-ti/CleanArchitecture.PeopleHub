using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.Dtos.IndividualPerson;

public class IndividualPersonDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public Cpf Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
    public Phone Phone { get; set; }
    public Email Email { get; set; }
    public string? Photo { get; set; } 

    public IndividualPersonDto(IndividualPersonEntity entity)
    {
        Id = entity.PersonId;
        FullName = entity.FullName;
        Cpf = entity.Cpf;
        BirthDate = entity.BirthDate;
        Gender = entity.Gender;
        Address = entity.Address;
        Phone = entity.Phone;
        Email = entity.Email;
        Photo = entity.Photo is { Length: > 0 } ? ConvertPhotoToSvg(entity.Photo) : null;
    }

    private static string ConvertPhotoToSvg(byte[] photo)
    {
        string base64 = Convert.ToBase64String(photo);
        return $"<svg xmlns='http://www.w3.org/2000/svg' width='100' height='100'>\n" +
               $"<image href='data:image/png;base64,{base64}' width='100' height='100' />\n" +
               $"</svg>";
    }
}
