using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseLoggingUseCase  
{
    protected readonly ILogger<BaseLoggingUseCase> _logger;
    protected readonly string _contextName;

    protected BaseLoggingUseCase() 
    {

        _logger = NullLogger<BaseLoggingUseCase>.Instance; 
    }
}
