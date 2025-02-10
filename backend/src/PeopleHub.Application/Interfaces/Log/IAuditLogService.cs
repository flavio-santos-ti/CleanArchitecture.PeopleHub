using PeopleHub.Application.Dtos.Log;

namespace PeopleHub.Application.Interfaces.Log;

public interface IAuditLogService
{
    Task RegisterLogAsync(AuditLogDto logDto);
}
