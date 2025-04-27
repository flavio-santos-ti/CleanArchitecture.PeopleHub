using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseLoggingUseCase  
{
    protected readonly ILogger<BaseLoggingUseCase> _logger;
    protected readonly string _contextName;

    protected BaseLoggingUseCase(
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService) 
    {

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
