using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IGetIndividualByCpfUseCase
{
    Task<Response<IndividualPersonDto?>> ExecuteAsync(string cpf);
}
