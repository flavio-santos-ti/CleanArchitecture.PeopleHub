﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.LegalPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.Person;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Providers;
using PeopleHub.Application.Routers;
using PeopleHub.Application.Services;
using PeopleHub.Application.UseCases.IndividualPerson;
using PeopleHub.Application.UseCases.LegalPerson;
using PeopleHub.Application.UseCases.Person;
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

            services.AddScoped<IRegisterUserAccountUseCase>(provider =>
            {
                return new RegisterUserAccountUseCase(
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

            services.AddScoped<IRegisterLegalPersonUseCase>(provider =>
            {
                return new RegisterLegalPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson") 
                );
            });

            services.AddScoped<IDeleteLegalPersonUseCase>(provider =>
            {
                return new DeleteLegalPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson")
                );
            });

            services.AddScoped<IUpdateLegalPersonUseCase>(provider =>
            {
                return new UpdateLegalPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("LegalPerson")
                );
            });

            services.AddScoped<IRegisterIndividualPersonUseCase>(provider =>
            {
                return new RegisterIndividualPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IUpdateIndividualPersonUseCase>(provider =>
            {
                return new UpdateIndividualPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IDeleteIndividualPersonUseCase>(provider =>
            {
                return new DeleteIndividualPersonUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    new FixedContextProvider("IndividualPerson")
                );
            });

            services.AddScoped<IGetIndividualPersonByCpfUseCase>(provider =>
            {
                return new GetIndividualPersonByCpfUseCase(
                    provider.GetRequiredService<IPersonRepository>(),
                    provider.GetRequiredService<IAuditLogService>(),
                    provider.GetRequiredService<IHttpContextAccessor>(),
                    provider.GetRequiredService<IAuthenticatedUserAccountService>(),
                    new FixedContextProvider("individualPerson")
                );
            });

            services.AddScoped<IUploadPersonPhotoUseCase>(provider =>
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
            services.AddScoped<IPersonRouter, PersonRouter>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IAuditLogService, AuditLogService>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            return services;
        }
    }
}
