namespace PeopleHub.Application.Dtos.Log;

public class AuditLogDto
{
    public string UserEmail { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string ContextName { get; set; } = string.Empty;
    public int HttpStatusCode { get; set; }
    public Guid? EntityId { get; set; }
    public string? EventData { get; set; }
    public string UserIp { get; set; } = "Unknown IP";
}
