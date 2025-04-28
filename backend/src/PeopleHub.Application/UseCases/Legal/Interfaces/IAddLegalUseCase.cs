using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.LegalPerson;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IAddLegalUseCase
{
    Task<Response<bool>> ExecuteAsync(RegisterLegalPersonRequestDto request);
}