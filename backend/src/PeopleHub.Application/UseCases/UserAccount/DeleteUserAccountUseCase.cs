using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(
        IUserAccountRepository userAccountRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
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
                return Result.CreateNotFound<bool>(NotFoundMessages.Feminine(EntityNames.UserAccount));

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            await _userAccountRepository.DeleteAsync(request.Email);
            await _unitOfWork.CommitAsync();

            return Result.CreateRemove<bool>("Conta de usuário excluída com sucesso.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
