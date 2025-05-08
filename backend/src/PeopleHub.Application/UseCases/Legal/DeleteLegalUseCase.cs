using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Legal;

public class DeleteLegalUseCase : IDeleteLegalUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLegalUseCase(
        IPersonOldRepository personRepository, 
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
            var person = await _personRepository.GetByCnpjAsync(request.Cnpj);
            if (person == null)
                return Result.CreateNotFound<bool>(NotFoundMessages.Feminine("Pessoa jurídica"));

            await _personRepository.DeleteAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Pessoa jurídica excluída com sucesso.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
