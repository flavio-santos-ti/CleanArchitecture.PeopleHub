using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IAuthenticateUserAccountUseCase
{
    Task<ApiResponseDto<object>> ExecuteAsync(UserAccountLoginDto request);
}