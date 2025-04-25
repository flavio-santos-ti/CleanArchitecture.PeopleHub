using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IUpdateLegalUseCase
{
    Task<Response<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request);
}
