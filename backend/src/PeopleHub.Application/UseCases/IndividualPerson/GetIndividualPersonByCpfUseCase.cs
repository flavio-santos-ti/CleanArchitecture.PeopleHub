using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.Log;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.IndividualPerson;

public class GetIndividualPersonByCpfUseCase : IGetIndividualPersonByCpfUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthenticatedUserAccountService _authenticatedUserService;


    public GetIndividualPersonByCpfUseCase(IPersonRepository personRepository, IAuditLogService auditLogService, IHttpContextAccessor httpContextAccessor, IAuthenticatedUserAccountService authenticatedUserService)
    {
        _personRepository = personRepository;
        _auditLogService = auditLogService;
        _httpContextAccessor = httpContextAccessor;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<IndividualPersonEntity?> ExecuteAsync(string cpf)
    {
        var person = await _personRepository.GetIndividualByCpfAsync(cpf);

        // Captures information from the HTTP context
        var httpContext = _httpContextAccessor.HttpContext;
        var userIp = httpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        var userEmail = _authenticatedUserService.GetAuthenticatedUserEmail() ?? "Unknown User"; // Gets the authenticated email

        // Register audit log
        await _auditLogService.RegisterLogAsync(new AuditLogDto
        {
            UserEmail = userEmail,
            Action = "READ",
            ContextName = nameof(IndividualPersonEntity),
            EntityId = person?.Id,
            EventData = null,
            UserIp = userIp 
        });

        return person;
    }
}
