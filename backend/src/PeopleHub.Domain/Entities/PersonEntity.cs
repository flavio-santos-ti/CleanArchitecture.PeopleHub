namespace PeopleHub.Domain.Entities;

public class PersonEntity
{
    public Guid Id { get; private set; }

    public char PersonTypeCode { get; private set; }

    // Construtor sem parâmetros requerido pelo Entity Framework Core
    // Parameterless constructor required by Entity Framework Core
    private PersonEntity()
    {
        Id = Guid.NewGuid();
        PersonTypeCode = 'F'; // Valor padrão, pode ser sobrescrito / Default value, can be overridden
    }

    // Construtor principal para instanciar uma nova pessoa
    // Main constructor to instantiate a new person
    public PersonEntity(char personTypeCode)
    {
        Id = Guid.NewGuid();
        PersonTypeCode = personTypeCode;

        Validate();
    }

    // Atualiza o tipo da pessoa ('F' ou 'J')
    // Updates the person type ('F' or 'J')
    public void UpdatePersonType(char newPersonTypeCode)
    {
        PersonTypeCode = newPersonTypeCode;
        Validate();
    }

    // Valida se o tipo de pessoa é válido
    // Validates if the person type is valid
    private void Validate()
    {
        if (PersonTypeCode != 'F' && PersonTypeCode != 'J')
            throw new ArgumentException("Person type must be 'F' or 'J'.");
    }
}
