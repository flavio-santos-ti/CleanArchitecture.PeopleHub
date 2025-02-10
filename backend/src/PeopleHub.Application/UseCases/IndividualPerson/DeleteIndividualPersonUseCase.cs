﻿using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.IndividualPerson;

public class DeleteIndividualPersonUseCase : BaseAuditableUseCase, IDeleteIndividualPersonUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteIndividualPersonUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuthenticatedUserAccountService authenticatedUserService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuditLogService auditLogService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<bool>> ExecuteAsync(DeleteIndividualPersonDto request)
    {
        try
        {
            var person = await _personRepository.GetIndividualByCpfAsync(request.Cpf);
            if (person == null)
                if (person == null)
                    return await AuditNotFoundErrorAsync<bool>(
                        eventValue: request,
                        message: "Individual Person not found."
                    );

            await _personRepository.DeleteIndividualAsync(person);
            await _unitOfWork.CommitAsync();

            // Register audit log
            return await DeleteSuccessWithAudit<bool>(
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
