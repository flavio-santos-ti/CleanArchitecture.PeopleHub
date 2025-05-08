namespace PeopleHub.Application.Messages
{
    public static class NotFoundMessages
    {
        public static string Feminine(string entity) => $"{entity} não encontrada.";
        public static string Masculine(string entity) => $"{entity} não encontrado.";
    }
}
