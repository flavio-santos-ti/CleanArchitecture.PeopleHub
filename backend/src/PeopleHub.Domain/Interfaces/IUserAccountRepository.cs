using PeopleHub.Domain.Entities;

namespace PeopleHub.Domain.Interfaces;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccountEntity user);
    Task<UserAccountEntity?> GetByEmailAsync(string email);
    Task UpdateAsync(UserAccountEntity user);
    Task DeleteAsync(string email);
}
