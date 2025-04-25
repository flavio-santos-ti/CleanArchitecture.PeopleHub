using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IRegisterUserAccountUseCase
{
    Task<Response<UserAccountEntity>> ExecuteAsync(RegisterUserAccountDto request);
}