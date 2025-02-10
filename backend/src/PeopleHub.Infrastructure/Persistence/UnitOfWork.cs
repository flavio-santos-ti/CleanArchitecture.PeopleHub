using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly PeopleHubDbContext _context;

    public UnitOfWork(PeopleHubDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}