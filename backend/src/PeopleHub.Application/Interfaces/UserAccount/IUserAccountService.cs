using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IUserAccountService
{
    Task<ApiResponseDto<bool>> RegisterAsync(RegisterUserAccountDto request);
    Task<ApiResponseDto<object>> AuthenticateAsync(UserAccountLoginDto request);
    Task<ApiResponseDto<bool>> UpdateAsync(UpdateUserAccountDto request);
    Task<ApiResponseDto<bool>> DeleteAsync(DeleteUserAccountDto request);
}