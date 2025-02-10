using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Interfaces.UserAccount;
using System.Security.Claims;

namespace PeopleHub.Infrastructure.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetAuthenticatedUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
