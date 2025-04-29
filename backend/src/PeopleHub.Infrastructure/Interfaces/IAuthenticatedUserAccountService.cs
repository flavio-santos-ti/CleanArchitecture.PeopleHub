namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IAuthenticatedUserAccountService
{
    string? GetAuthenticatedUserEmail();
}