using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IRegisterUserAccountUseCase _registerUserAccountUseCase;
    private readonly IAuthenticateUserAccountUseCase _authenticateUserAccountUseCase;
    private readonly IUpdateUserAccountUseCase _updateUserAccountUseCase;
    private readonly IDeleteUserAccountUseCase _deleteUserAccountUseCase;

    public UserAccountService(
        IRegisterUserAccountUseCase registerUserAccountUseCase,
        IAuthenticateUserAccountUseCase authenticateUserAccountUseCase,
        IUpdateUserAccountUseCase updateUserAccountUseCase,
        IDeleteUserAccountUseCase deleteUserAccountUseCase)
    {
        _registerUserAccountUseCase = registerUserAccountUseCase;
        _authenticateUserAccountUseCase = authenticateUserAccountUseCase;
        _updateUserAccountUseCase = updateUserAccountUseCase;
        _deleteUserAccountUseCase = deleteUserAccountUseCase;
    }

    public async Task<Response<UserAccountEntity>> RegisterAsync(RegisterUserAccountDto request)
    {
        return await _registerUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<Response<object>> AuthenticateAsync(UserAccountLoginDto request)
    {
        return await _authenticateUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> UpdateAsync(UpdateUserAccountDto request)
    {
        return  await _updateUserAccountUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> DeleteAsync(DeleteUserAccountDto request)
    {
        return await _deleteUserAccountUseCase.ExecuteAsync(request);
    }
}
