using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly PeopleHubDbContext _context;

    public UserAccountRepository(PeopleHubDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserAccountEntity user)
    {
        await _context.UserAccounts.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserAccountEntity?> GetByEmailAsync(string email)
    {
        return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateAsync(UserAccountEntity user)
    {
        _context.UserAccounts.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string email)
    {
        var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            _context.UserAccounts.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}