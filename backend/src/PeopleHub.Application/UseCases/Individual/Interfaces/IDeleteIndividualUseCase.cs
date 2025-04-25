using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IDeleteIndividualUseCase
{
    Task<Response<bool>> ExecuteAsync(DeleteIndividualPersonDto request);
}
