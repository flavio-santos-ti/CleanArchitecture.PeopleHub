using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IUserAccountService
{
    Task<Response<UserAccountEntity>> RegisterAsync(RegisterUserAccountDto request);
    Task<Response<object>> AuthenticateAsync(UserAccountLoginDto request);
    Task<ApiResponseDto<bool>> UpdateAsync(UpdateUserAccountDto request);
    Task<Response<bool>> DeleteAsync(DeleteUserAccountDto request);
}