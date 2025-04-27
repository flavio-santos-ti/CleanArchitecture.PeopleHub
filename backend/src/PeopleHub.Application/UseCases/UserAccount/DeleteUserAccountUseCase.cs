using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class DeleteUserAccountUseCase : BaseLoggingUseCase, IDeleteUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(
        IUserAccountRepository userAccountRepository, 
        IUnitOfWork unitOfWork, 
        IAuditLogService auditLogService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base()
    {
        _userAccountRepository = userAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(DeleteUserAccountDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result.CreateNotFound<bool>("User not found.");

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Account has been successfully removed.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
