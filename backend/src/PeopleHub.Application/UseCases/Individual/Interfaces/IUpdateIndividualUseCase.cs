using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IUpdateIndividualUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UpdateIndividualPersonRequestDto request);
}
