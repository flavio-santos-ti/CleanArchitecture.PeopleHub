using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.LegalPerson;

public interface IUpdateLegalPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request);
}
