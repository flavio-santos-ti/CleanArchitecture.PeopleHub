using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IUpdateLegalUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request);
}
