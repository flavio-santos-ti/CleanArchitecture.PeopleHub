namespace PeopleHub.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}