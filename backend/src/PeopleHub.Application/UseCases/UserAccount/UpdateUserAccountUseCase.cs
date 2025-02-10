using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class UpdateUserAccountUseCase : BaseAuditableUseCase, IUpdateUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserAccountUseCase(
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

    public async Task<ApiResponseDto<bool>> ExecuteAsync(UpdateUserAccountDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return await AuditNotFoundErrorAsync<bool>(
                    eventValue: request,
                    message: "User not found."
                );

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
                return await AuditValidationErrorAsync<bool>(
                    eventValue: request,
                    message: "Invalid email or password."
                );

            user.UpdatePassword(request.NewPassword);

            await _userAccountRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return await UpdateSuccessWithAudit<bool>(
                eventValue: request,
                oldValue: user,
                message: "Password updated successfully."
            );
        }
        catch (Exception ex)
        {
            return await AuditExceptionAsync<bool>(message: ex.Message);
        }
    }
}
