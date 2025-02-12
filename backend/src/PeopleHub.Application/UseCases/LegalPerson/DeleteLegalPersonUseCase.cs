using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.LegalPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.LegalPerson;

public class DeleteLegalPersonUseCase : BaseAuditableUseCase, IDeleteLegalPersonUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLegalPersonUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuditLogService auditLogService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<bool>> ExecuteAsync(DeleteLegalPersonDto request)
    {
        try
        {
            var person = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);
            if (person == null)
                return await AuditNotFoundErrorAsync<bool>(
                    eventValue: request,
                    message: "Legal Person not found."
                );

            await _personRepository.DeleteLegalAsync(person);
            await _unitOfWork.CommitAsync();

            return await ResponseAsync<bool>(
                logAction: Actions.LogAction.DELETE,
                eventValue: request,
                oldValue: person,
                message: "Legal Person has been successfully removed."
            );
        }
        catch (Exception ex)
        {
            return await AuditExceptionAsync<bool>(message: ex.Message);
        }
    }
}
