using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IGetIndividualByCpfUseCase
{
    Task<ApiResponseDto<IndividualPersonDto?>> ExecuteAsync(string cpf);
}
