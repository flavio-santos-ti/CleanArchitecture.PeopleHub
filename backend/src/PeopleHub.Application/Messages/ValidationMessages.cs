namespace PeopleHub.Application.Messages;

public static class ValidationMessages
{
    public static string AlreadyRegisteredFeminine(string entity) => $"{entity} já cadastrada.";
    public static string AlreadyRegisteredMasculine(string entity) => $"{entity} já cadastrado.";
}
