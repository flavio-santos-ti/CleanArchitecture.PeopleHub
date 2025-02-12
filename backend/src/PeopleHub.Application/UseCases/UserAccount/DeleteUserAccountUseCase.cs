using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class DeleteUserAccountUseCase : BaseAuditableUseCase, IDeleteUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(
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

    public async Task<ApiResponseDto<bool>> ExecuteAsync(DeleteUserAccountDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return await ResponseAsync<bool>(
                    logAction: LogAction.NOT_FOUND,
                    eventValue: request,
                    message: "User not found."
                );

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            return await ResponseAsync<bool>(
                logAction: LogAction.DELETE,
                eventValue: request,
                oldValue: user,
                message: "Account has been successfully removed."
            );
        }
        catch (Exception ex)
        {
            return await ResponseAsync<bool>(logAction: LogAction.ERROR, message: ex.Message);
        }
    }
}
