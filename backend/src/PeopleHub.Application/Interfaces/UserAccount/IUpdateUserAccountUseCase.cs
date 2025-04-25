using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IUpdateUserAccountUseCase
{
    Task<Response<bool>> ExecuteAsync(UpdateUserAccountDto request);
}
