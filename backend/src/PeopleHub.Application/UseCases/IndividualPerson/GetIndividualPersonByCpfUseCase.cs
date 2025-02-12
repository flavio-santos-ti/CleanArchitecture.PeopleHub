using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.IndividualPerson;

public class GetIndividualPersonByCpfUseCase : BaseLoggingUseCase, IGetIndividualPersonByCpfUseCase
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

    public async Task<ApiResponseDto<IndividualPersonDto?>> ExecuteAsync(string cpf)
    {
        var cleanedCpf = Cpf.Clean(cpf);
        var validationError = Cpf.Validate(cleanedCpf);

        if (validationError != null)
            return await ResponseAsync<IndividualPersonDto?>(
                logAction: LogAction.VALIDATION_ERROR,
                eventValue: cleanedCpf,
                message: validationError
            );

        var person = await _personRepository.GetIndividualByCpfAsync(cleanedCpf);

        if (person == null)
            return await ResponseAsync<IndividualPersonDto?>(
                logAction: LogAction.NOT_FOUND,
                eventValue: cpf,
                message: "Individual Person not found."
            );

        var personDto = new IndividualPersonDto(person);

        return await ResponseAsync<IndividualPersonDto?>(
            logAction: LogAction.READ,
            eventValue: person,
            message: "Individual person retrieved successfully.",
            data: personDto
        );
    }
}
