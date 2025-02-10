using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IUpdateIndividualPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UpdateIndividualPersonRequestDto request);
}
