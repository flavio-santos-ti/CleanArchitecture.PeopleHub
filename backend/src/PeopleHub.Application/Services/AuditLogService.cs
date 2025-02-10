using PeopleHub.Application.Dtos.Log;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository;

    public AuditLogService(IAuditLogRepository auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    public async Task RegisterLogAsync(AuditLogDto logDto)
    {
        var log = new AuditLogEntity(
            logDto.UserEmail,
            logDto.Action,
            logDto.ContextName,
            logDto.HttpStatusCode,
            logDto.EventData,
            logDto.UserIp
        );

        await _auditLogRepository.AddAsync(log);
    }
}
