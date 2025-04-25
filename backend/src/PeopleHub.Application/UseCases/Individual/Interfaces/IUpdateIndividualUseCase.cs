using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.IndividualPerson;

namespace PeopleHub.Application.UseCases.Individual.Interfaces;

public interface IUpdateIndividualUseCase
{
    Task<Response<bool>> ExecuteAsync(UpdateIndividualPersonRequestDto request);
}
