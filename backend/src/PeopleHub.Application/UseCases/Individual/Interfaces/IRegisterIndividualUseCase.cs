using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IRegisterIndividualUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(RegisterIndividualPersonRequestDto request);
}