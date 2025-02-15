using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IRegisterLegalUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(RegisterLegalPersonRequestDto request);
}