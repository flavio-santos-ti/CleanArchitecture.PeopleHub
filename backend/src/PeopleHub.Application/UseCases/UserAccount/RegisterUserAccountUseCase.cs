using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class RegisterUserAccountUseCase : BaseAuditableUseCase, IRegisterUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserAccountUseCase(
        IUserAccountRepository userAccountRepository,
        IUnitOfWork unitOfWork,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _userAccountRepository = userAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<bool>> ExecuteAsync(RegisterUserAccountDto request)
    {
        try
        {
            var existingUser = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return await AuditLoginValidationErrorAsync<bool>(
                    eventValue: request,
                    message: "User already exists.",
                    userEmail: request.Email
                );

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new UserAccountEntity(request.Email, hashedPassword);

            await _userAccountRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return await CreateLoginSuccessWithAudit<bool>(
                eventValue: user,
                message: "User registered successfully.",
                userEmail: request.Email
            );
        }
        catch (Exception ex)
        {
            return await AuditExceptionAsync<bool>(message: ex.Message);
        }
    }
}
