using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class RegisterUserAccountUseCase : BaseLoggingUseCase, IRegisterUserAccountUseCase
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

    public async Task<Response<UserAccountEntity>> ExecuteAsync(RegisterUserAccountDto request)
    {
        try
        {
            var existingUser = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return Result.CreateValidationError<UserAccountEntity>("User already exists.");


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new UserAccountEntity(request.Email, hashedPassword);

            await _userAccountRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd("User registered successfully.", user);
        }
        catch (Exception ex)
        {
            return Result.CreateError<UserAccountEntity>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
