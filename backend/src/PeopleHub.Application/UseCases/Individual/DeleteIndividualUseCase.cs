using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Individual;

public class DeleteIndividualUseCase : IDeleteIndividualUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteIndividualUseCase(
        IPersonOldRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuthenticatedUserAccountService authenticatedUserService, 
        IHttpContextAccessor httpContextAccessor, 
        IContextProvider contextProvider) 
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(DeleteIndividualPersonDto request)
    {
        try
        {
            var person = await _personRepository.GetByCpfAsync(request.Cpf);
            if (person == null)
                return Result.CreateNotFound<bool>(NotFoundMessages.Feminine("Pessoa física"));

            await _personRepository.DeleteAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Pessoa física excluída com sucesso.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
