using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Infrastructure.Persistence;
using PeopleHub.Infrastructure.Persistence.Repositories;
using PeopleHub.Infrastructure.Services;

namespace PeopleHub.Infrastructure.Configurations;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Separate database configuration
        services.AddDatabaseConfiguration(configuration);

        // Repositories
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IAuthenticatedUserAccountService, AuthenticatedUserService>();
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}