using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Individual;

public class DeleteIndividualUseCase : BaseLoggingUseCase, IDeleteIndividualUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteIndividualUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuthenticatedUserAccountService authenticatedUserService, 
        IHttpContextAccessor httpContextAccessor, 
        //IAuditLogService auditLogService,
        IContextProvider contextProvider) 
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(DeleteIndividualPersonDto request)
    {
        try
        {
            var person = await _personRepository.GetIndividualByCpfAsync(request.Cpf);
            if (person == null)
                return Result.CreateNotFound<bool>("Individual Person not found.");

            await _personRepository.DeleteIndividualAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Legal Person has been successfully removed.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
