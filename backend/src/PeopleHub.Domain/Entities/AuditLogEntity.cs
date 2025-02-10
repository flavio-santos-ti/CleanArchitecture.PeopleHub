namespace PeopleHub.Domain.Entities;

public class AuditLogEntity
{
    public Guid Id { get; private set; }
    public string UserEmail { get; private set; }
    public string UserIp { get; private set; }
    public string EventAction { get; private set; }
    public string ContextName { get; private set; }
    public int HttpStatusCode { get; private set; }
    public string? EventData { get; private set; }
    public DateTime EventTimestamp { get; private set; }

    public AuditLogEntity(string userEmail, string eventAction, string contextName, int httpStatusCode, string? eventData, string userIp)
    {
        Id = Guid.NewGuid();
        UserEmail = userEmail;
        EventAction = eventAction;
        ContextName = contextName;
        HttpStatusCode = httpStatusCode;
        EventData = eventData;
        UserIp = userIp;
        EventTimestamp = DateTime.Now;
    }
}
