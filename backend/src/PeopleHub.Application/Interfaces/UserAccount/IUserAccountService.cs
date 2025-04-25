using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IUserAccountService
{
    Task<FDS.NetCore.ApiResponse.Models.Response<UserAccountEntity>> RegisterAsync(RegisterUserAccountDto request);
    Task<FDS.NetCore.ApiResponse.Models.Response<object>> AuthenticateAsync(UserAccountLoginDto request);
    Task<Response<bool>> UpdateAsync(UpdateUserAccountDto request);
    Task<FDS.NetCore.ApiResponse.Models.Response<bool>> DeleteAsync(DeleteUserAccountDto request);
}