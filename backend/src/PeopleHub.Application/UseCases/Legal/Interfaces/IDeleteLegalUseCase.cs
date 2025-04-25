using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.LegalPerson;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IDeleteLegalUseCase
{
    Task<Response<bool>> ExecuteAsync(DeleteLegalPersonDto request);
}
