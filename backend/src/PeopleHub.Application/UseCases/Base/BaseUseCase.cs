using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseUseCase
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IAuthenticatedUserAccountService _authenticatedUserService;

    protected BaseUseCase(IHttpContextAccessor httpContextAccessor, IAuthenticatedUserAccountService authenticatedUserService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authenticatedUserService = authenticatedUserService;
    }

    protected string GetAuthenticatedUserEmail()
    {
        return _authenticatedUserService.GetAuthenticatedUserEmail() ?? "Trying a New User";
    }
}
