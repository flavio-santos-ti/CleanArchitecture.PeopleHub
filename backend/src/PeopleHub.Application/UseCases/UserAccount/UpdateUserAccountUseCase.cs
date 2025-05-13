using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class UpdateUserAccountUseCase : IUpdateUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserAccountUseCase(
        IUserAccountRepository userAccountRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base()
    {
        _userAccountRepository = userAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(UpdateUserAccountDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result.CreateNotFound<bool>(NotFoundMessages.Feminine(EntityNames.UserAccount));

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
                return Result.CreateValidationError<bool>("Email ou senha inválida.");

            user.UpdatePassword(request.NewPassword);

            await _userAccountRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.CreateModify<bool>(SuccessMessages.UpdatedFeminine(EntityNames.Password));
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
