using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IDeleteUserAccountUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(DeleteUserAccountDto request);
}
