using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;

namespace PeopleHub.Application.Interfaces.UserAccount;

public interface IUpdateUserAccountUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UpdateUserAccountDto request);
}
