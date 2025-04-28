using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.IndividualPerson;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IAddIndividualUseCase
{
    Task<Response<bool>> ExecuteAsync(AddIndividualPersonRequestDto request);
}