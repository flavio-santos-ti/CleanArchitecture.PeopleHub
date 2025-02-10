using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.IndividualPerson;

public class GetIndividualPersonByCpfUseCase : BaseAuditableUseCase, IGetIndividualPersonByCpfUseCase
{
    private readonly IPersonRepository _personRepository;


    public GetIndividualPersonByCpfUseCase(
        IPersonRepository personRepository, 
        IAuditLogService auditLogService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _personRepository = personRepository;
    }

    public async Task<ApiResponseDto<IndividualPersonEntity?>> ExecuteAsync(string cpf)
    {
        var cleanedCpf = Cpf.Clean(cpf);
        var validationError = Cpf.Validate(cleanedCpf);

        if (validationError != null)
            return await AuditLoginValidationErrorAsync<IndividualPersonEntity?>(
                eventValue: cleanedCpf,
                message: validationError
            );

        var person = await _personRepository.GetIndividualByCpfAsync(cleanedCpf);

        if (person == null)
            return await AuditNotFoundErrorAsync<IndividualPersonEntity?>(
                eventValue: cpf,
                message: "Individual person not found."
            );

        return await ReadSuccessWithAudit<IndividualPersonEntity?>(
            eventValue: person,
            message: "Individual person retrieved successfully.",
            data: person
        );
    }
}
