namespace PeopleHub.Application.Messages;

public static class SuccessMessages
{
    public static string CreatedFeminine(string entity) => $"{entity} cadastrada com sucesso.";
    public static string CreatedMasculine(string entity) => $"{entity} cadastrado com sucesso.";

    public static string UpdatedFeminine(string entity) => $"{entity} atualizada com sucesso.";
    public static string UpdatedMasculine(string entity) => $"{entity} atualizado com sucesso.";

    public static string RemovedFeminine(string entity) => $"{entity} excluída com sucesso.";
    public static string RemovedMasculine(string entity) => $"{entity} excluído com sucesso.";

    public static string RetrievedFeminine(string entity) => $"{entity} recuperada com sucesso.";
    public static string RetrievedMasculine(string entity) => $"{entity} recuperado com sucesso.";
}
