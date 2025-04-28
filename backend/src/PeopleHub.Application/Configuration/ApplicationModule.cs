using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.Person;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Providers;
using PeopleHub.Application.Services;
using PeopleHub.Application.UseCases.Individual;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Application.UseCases.Legal;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Application.UseCases.Photo;
using PeopleHub.Application.UseCases.UserAccount;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.Configuration
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContextProvider>(provider => new FixedContextProvider("DefaultContext"));

            // Registration of UseCases
            services.AddScoped<IAuthenticateUserAccountUseCase>(provider =>
            {
                return new AuthenticateUserAccountUseCase(
                    provider.GetRequiredService<IUserAccountRepository>(),
                    provider.GetRequiredService<IConfiguration>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("Login")
                );
            });

            services.AddScoped<IAddUserAccountUseCase>(provider =>
            {
                return new AddUserAccountUseCase(
                    provider.GetRequiredService<IUserAccountRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("UserAccount")
                );
            });

            services.AddScoped<IUpdateUserAccountUseCase>(provider =>
            {
                return new UpdateUserAccountUseCase(
                    provider.GetRequiredService<IUserAccountRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("UserAccount")
                );
            });

            services.AddScoped<IDeleteUserAccountUseCase>(provider =>
            {
                return new DeleteUserAccountUseCase(
                    provider.GetRequiredService<IUserAccountRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("UserAccount")
                );
            });

            services.AddScoped<IAddLegalUseCase>(provider =>
            {
                return new AddLegalUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson") 
                );
            });

            services.AddScoped<IDeleteLegalUseCase>(provider =>
            {
                return new DeleteLegalUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson")
                );
            });

            services.AddScoped<IUpdateLegalUseCase>(provider =>
            {
                return new UpdateLegalUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson")
                );
            });

            services.AddScoped<IAddIndividualUseCase>(provider =>
            {
                return new AddIndividualUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IUpdateIndividualUseCase>(provider =>
            {
                return new UpdateIndividualUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IDeleteIndividualUseCase>(provider =>
            {
                return new DeleteIndividualUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IGetIndividualByCpfUseCase>(provider =>
            {
                return new GetIndividualByCpfUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("individualPerson")
                );
            });

            services.AddScoped<IUploadPhotoUseCase>(provider =>
            {
                return new UploadPersonPhotoUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("UploadPerson")
                );
            });

            // Registration of Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IAuditLogService, AuditLogService>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            return services;
        }
    }
}
