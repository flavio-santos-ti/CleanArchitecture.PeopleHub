using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IAddUserAccountUseCase _addUserAccountUseCase;
    private readonly IAuthenticateUserAccountUseCase _authenticateUserAccountUseCase;
    private readonly IUpdateUserAccountUseCase _updateUserAccountUseCase;
    private readonly IDeleteUserAccountUseCase _deleteUserAccountUseCase;

    public UserAccountService(
        IAddUserAccountUseCase addUserAccountUseCase,
        IAuthenticateUserAccountUseCase authenticateUserAccountUseCase,
        IUpdateUserAccountUseCase updateUserAccountUseCase,
        IDeleteUserAccountUseCase deleteUserAccountUseCase)
    {
        _addUserAccountUseCase = addUserAccountUseCase;
        _authenticateUserAccountUseCase = authenticateUserAccountUseCase;
        _updateUserAccountUseCase = updateUserAccountUseCase;
        _deleteUserAccountUseCase = deleteUserAccountUseCase;
    }

    public async Task<FDS.NetCore.ApiResponse.Models.Response<UserAccountEntity>> RegisterAsync(RegisterUserAccountDto request)
    {
        return await _addUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<FDS.NetCore.ApiResponse.Models.Response<object>> AuthenticateAsync(UserAccountLoginDto request)
    {
        return await _authenticateUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> UpdateAsync(UpdateUserAccountDto request)
    {
        return  await _updateUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<FDS.NetCore.ApiResponse.Models.Response<bool>> DeleteAsync(DeleteUserAccountDto request)
    {
        return await _deleteUserAccountUseCase.ExecuteAsync(request);
    }
}
