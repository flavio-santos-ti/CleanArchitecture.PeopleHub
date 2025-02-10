using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IRegisterIndividualPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(RegisterIndividualPersonRequestDto request);
}