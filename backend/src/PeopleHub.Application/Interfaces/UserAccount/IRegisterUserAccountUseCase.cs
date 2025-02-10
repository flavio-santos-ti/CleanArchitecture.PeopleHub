using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IRegisterUserAccountUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(RegisterUserAccountDto request);
}