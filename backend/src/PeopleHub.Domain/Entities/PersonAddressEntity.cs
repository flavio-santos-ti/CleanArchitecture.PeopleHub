namespace PeopleHub.Domain.Entities;

public class PersonAddressEntity
{
    public Guid Id { get; private set; }
    public Guid PersonId { get; private set; }
    public char AddressTypeCode { get; private set; }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public string Phone { get; private set; }
    public string Email { get; private set; }

    public bool IsActive { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    // Construtor sem parâmetros requerido pelo Entity Framework Core
    // Parameterless constructor required by Entity Framework Core
    private PersonAddressEntity()
    {
        Id = Guid.NewGuid();
        Street = string.Empty;
        Number = string.Empty;
        Complement = string.Empty;
        City = string.Empty;
        State = string.Empty;
        ZipCode = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        IsActive = true;
    }

    // Construtor principal
    // Main constructor
    public PersonAddressEntity(
        Guid personId,
        char addressTypeCode,
        string street,
        string number,
        string? complement,
        string city,
        string state,
        string zipCode,
        string phone,
        string email
    )
    {
        Id = Guid.NewGuid();
        PersonId = personId;
        AddressTypeCode = addressTypeCode;
        Street = street;
        Number = number;
        Complement = complement;
        City = city;
        State = state;
        ZipCode = zipCode;
        Phone = phone;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    // Valida os campos obrigatórios
    // Validates required fields
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Street))
            throw new ArgumentException("Street is required.");
        if (string.IsNullOrWhiteSpace(Number))
            throw new ArgumentException("Number is required.");
        if (string.IsNullOrWhiteSpace(City))
            throw new ArgumentException("City is required.");
        if (string.IsNullOrWhiteSpace(State))
            throw new ArgumentException("State is required.");
        if (string.IsNullOrWhiteSpace(ZipCode))
            throw new ArgumentException("ZipCode is required.");
        if (string.IsNullOrWhiteSpace(Phone))
            throw new ArgumentException("Phone is required.");
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Email is required.");
    }

    // Atualiza os dados do endereço
    // Updates address data
    public void Update(
        string street,
        string number,
        string? complement,
        string city,
        string state,
        string zipCode,
        string phone,
        string email
    )
    {
        Street = street;
        Number = number;
        Complement = complement;
        City = city;
        State = state;
        ZipCode = zipCode;
        Phone = phone;
        Email = email;

        Validate();
    }

    // Inativa o endereço
    // Deactivates the address
    public void Deactivate()
    {
        IsActive = false;
    }
}
