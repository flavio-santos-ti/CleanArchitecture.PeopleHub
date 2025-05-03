namespace PeopleHub.Domain.Entities;

public class PersonTypeEntity
{
    public char Code { get; private set; }
    public string Description { get; private set; }

    // Construtor sem parâmetros requerido pelo Entity Framework Core
    // Parameterless constructor required by Entity Framework Core
    private PersonTypeEntity()
    {
        Code = 'F'; // Valor padrão, pode ser substituído / Default value, can be overridden
        Description = string.Empty;
    }

    // Construtor principal
    // Main constructor
    public PersonTypeEntity(char code, string description)
    {
        Code = code;
        Description = description;

        Validate();
    }

    // Valida os campos obrigatórios
    // Validates required fields
    private void Validate()
    {
        if (Code != 'F' && Code != 'J')
            throw new ArgumentException("Person type code must be 'F' or 'J'.");

        if (string.IsNullOrWhiteSpace(Description))
            throw new ArgumentException("Description is required.");
    }

    // Atualiza a descrição do tipo de pessoa
    // Updates the person type description
    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        Validate();
    }
}
