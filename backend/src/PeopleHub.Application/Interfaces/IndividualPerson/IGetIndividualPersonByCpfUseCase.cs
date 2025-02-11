using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IGetIndividualPersonByCpfUseCase
{
    Task<ApiResponseDto<IndividualPersonDto?>> ExecuteAsync(string cpf);
}
