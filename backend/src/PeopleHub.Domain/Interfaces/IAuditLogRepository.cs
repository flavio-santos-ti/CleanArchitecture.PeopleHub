using PeopleHub.Domain.Entities;

namespace PeopleHub.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLogEntity log);
}
