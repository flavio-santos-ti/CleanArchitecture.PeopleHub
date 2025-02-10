using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.LegalPerson;

public interface IDeleteLegalPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(DeleteLegalPersonDto request);
}
