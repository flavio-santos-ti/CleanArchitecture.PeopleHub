using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IRegisterIndividualUseCase
{
    Task<Response<bool>> ExecuteAsync(RegisterIndividualPersonRequestDto request);
}