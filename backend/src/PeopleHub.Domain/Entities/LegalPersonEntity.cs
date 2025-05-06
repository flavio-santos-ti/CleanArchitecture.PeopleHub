using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Domain.Entities;

public class LegalPersonEntity
{
    public Guid PersonId { get; private set; }
    public string LegalName { get; private set; } = string.Empty;
    public string TradeName { get; private set; } = string.Empty;
    public Cnpj Cnpj { get; private set; } = default!;
    public string StateRegistration { get; private set; } = string.Empty;
    public string MunicipalRegistration { get; private set; } = string.Empty;
    public Phone? Phone { get; private set; }
    public Email? Email { get; private set; }
    public string LegalRepresentativeName { get; private set; } = string.Empty;
    public Cpf? LegalRepresentativeCpf { get; private set; }
    public byte[] Logo { get; private set; } = Array.Empty<byte>();

    private LegalPersonEntity() { }

    public LegalPersonEntity(
        string legalName,
        string tradeName,
        Cnpj cnpj,
        string stateRegistration,
        string municipalRegistration,
        Phone phone,
        Email email,
        string legalRepresentativeName,
        Cpf legalRepresentativeCpf)
    {
        PersonId = Guid.NewGuid();
        LegalName = legalName;
        TradeName = tradeName;
        Cnpj = cnpj ?? throw new ArgumentNullException(nameof(cnpj));
        StateRegistration = stateRegistration;
        MunicipalRegistration = municipalRegistration;
        Phone = phone;
        Email = email;
        LegalRepresentativeName = legalRepresentativeName;
        LegalRepresentativeCpf = legalRepresentativeCpf ?? throw new ArgumentNullException(nameof(legalRepresentativeCpf));
        Logo = Array.Empty<byte>();

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(LegalName))
            throw new ArgumentException("Legal name is required.");
        if (Cnpj == null || Cnpj.IsEmpty())
            throw new ArgumentException("CNPJ is required.");
        if (string.IsNullOrWhiteSpace(LegalRepresentativeName))
            throw new ArgumentException("Legal representative name is required.");
        if (LegalRepresentativeCpf == null || LegalRepresentativeCpf.IsEmpty())
            throw new ArgumentException("Legal representative CPF is required.");
    }

    public void UpdateLegalPerson(
        string legalName,
        string tradeName,
        string stateRegistration,
        string municipalRegistration,
        Phone phone,
        Email email,
        string legalRepresentativeName,
        Cpf legalRepresentativeCpf)
    {
        LegalName = legalName;
        TradeName = tradeName;
        StateRegistration = stateRegistration;
        MunicipalRegistration = municipalRegistration;
        Phone = phone;
        Email = email;
        LegalRepresentativeName = legalRepresentativeName;
        LegalRepresentativeCpf = legalRepresentativeCpf;

        Validate(); // Revalidates the entity after the update.
    }

    public void UpdatePhoto(byte[] newPhoto)
    {
        if (newPhoto == null || newPhoto.Length == 0)
            throw new ArgumentException("Photo cannot be empty.");

        Logo = newPhoto;
    }
}
