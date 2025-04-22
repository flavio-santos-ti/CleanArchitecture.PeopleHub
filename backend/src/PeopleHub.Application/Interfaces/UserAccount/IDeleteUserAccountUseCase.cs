using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IDeleteUserAccountUseCase
{
    Task<Response<bool>> ExecuteAsync(DeleteUserAccountDto request);
}
