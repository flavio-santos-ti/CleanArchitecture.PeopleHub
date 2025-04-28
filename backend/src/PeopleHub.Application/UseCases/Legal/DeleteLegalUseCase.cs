using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Legal;

public class DeleteLegalUseCase : IDeleteLegalUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLegalUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(DeleteLegalPersonDto request)
    {
        try
        {
            var person = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);
            if (person == null)
                return Result.CreateNotFound<bool>("Legal Person not found.");

            await _personRepository.DeleteLegalAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Legal Person has been successfully removed.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
