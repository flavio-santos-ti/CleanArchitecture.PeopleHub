using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Domain.Entities;

public class IndividualPersonEntity
{
    public Guid PersonId { get; private set; }
    public string FullName { get; private set; }
    public Cpf Cpf { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public Address Address { get; private set; }
    public byte[] Photo { get; private set; }

    // Parameterless constructor required for Entity Framework Core
    private IndividualPersonEntity()
    {
        PersonId = Guid.NewGuid();
        FullName = string.Empty;
        Cpf = null!; 
        BirthDate = DateTime.MinValue;
        Gender = Gender.Other;

        Address = null!; 
        Photo = null!;
    }

    // Main constructor
    public IndividualPersonEntity(string fullName, Cpf cpf, DateTime birthDate, Gender gender, Address address, Phone phone, Email email)
    {
        PersonId = Guid.NewGuid();
        FullName = fullName;
        Cpf = cpf;
        BirthDate = birthDate;
        Gender = gender;
        Address = address;
        Photo = Array.Empty<byte>();

        Validate();
    }

    public void UpdatePhoto(byte[] newPhoto)
    {
        if (newPhoto == null || newPhoto.Length == 0)
        {
            throw new ArgumentException("Photo cannot be empty.");
        }
        Photo = newPhoto;
    }

    private void Validate()
    {
        if (string.IsNullOrEmpty(FullName))
            throw new ArgumentException("Full name is required.");
        if (BirthDate > DateTime.Now)
            throw new ArgumentException("Birth date cannot be in the future.");
    }

    public void UpdateIndividualPerson(
        string fullName,
        DateTime birthDate,
        Gender gender,
        Address address)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        Address = address;

        Validate(); 
    }

}
