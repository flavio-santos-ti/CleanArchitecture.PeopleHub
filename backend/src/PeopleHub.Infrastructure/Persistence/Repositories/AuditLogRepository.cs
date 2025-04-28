using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly PeopleHubDbContext _context;

    public AuditLogRepository(PeopleHubDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuditLogEntity log)
    {
        await _context.AuditLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}
