using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Log;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseLoggingUseCase : BaseUseCase
{
    protected readonly IAuditLogService _auditLogService;
    protected readonly ILogger<BaseLoggingUseCase> _logger;
    protected readonly string _contextName;

    protected BaseLoggingUseCase(
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(httpContextAccessor, authenticatedUserService)
    {
        _auditLogService = auditLogService;
        _contextName = contextProvider.ContextName;

        var serviceProvider = httpContextAccessor.HttpContext?.RequestServices;
        if (serviceProvider != null)
        {
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            _logger = loggerFactory?.CreateLogger<BaseLoggingUseCase>() ?? NullLogger<BaseLoggingUseCase>.Instance;
        }
        else
        {
            _logger = NullLogger<BaseLoggingUseCase>.Instance; 
        }
    }
}
