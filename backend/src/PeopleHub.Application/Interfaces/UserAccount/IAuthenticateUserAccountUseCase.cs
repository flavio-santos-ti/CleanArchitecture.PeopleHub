using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IAuthenticateUserAccountUseCase
{
    Task<Response<object>> ExecuteAsync(UserAccountLoginDto request);
}