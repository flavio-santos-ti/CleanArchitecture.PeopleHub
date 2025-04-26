using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.LegalPerson;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IUpdateLegalUseCase
{
    Task<Response<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request);
}
