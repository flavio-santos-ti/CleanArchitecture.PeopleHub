using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.LegalPerson;

public interface IRegisterLegalPersonUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(RegisterLegalPersonRequestDto request);
}