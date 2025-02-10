using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IDeleteIndividualPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(DeleteIndividualPersonDto request);
}
