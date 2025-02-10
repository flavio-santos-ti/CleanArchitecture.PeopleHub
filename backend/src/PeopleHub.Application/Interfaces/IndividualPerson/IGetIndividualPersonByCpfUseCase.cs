using PeopleHub.Application.Dtos.Response;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IGetIndividualPersonByCpfUseCase
{
    Task<ApiResponseDto<IndividualPersonEntity?>> ExecuteAsync(string cpf);
}
