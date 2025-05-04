namespace PeopleHub.Domain.Entities;

public class AddressTypeEntity
{
    public char Code { get; private set; }
    public string Description { get; private set; }

    // Construtor sem parâmetros requerido pelo Entity Framework Core
    // Parameterless constructor required by Entity Framework Core
    private AddressTypeEntity()
    {
        Code = 'R'; // Valor padrão, pode ser substituído / Default value, can be overridden
        Description = string.Empty;
    }

    // Construtor principal
    // Main constructor
    public AddressTypeEntity(char code, string description)
    {
        Code = code;
        Description = description;

        Validate();
    }

    // Valida os campos obrigatórios
    // Validates required fields
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Description))
            throw new ArgumentException("Description is required.");
    }

    // Atualiza a descrição do tipo de endereço
    // Updates the address type description
    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        Validate();
    }
}
