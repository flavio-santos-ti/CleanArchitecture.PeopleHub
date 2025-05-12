using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.UserAccount;

public class AddUserAccountUseCase : IAddUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserAccountUseCase(
        IUserAccountRepository userAccountRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
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
                return Result.CreateValidationError<UserAccountEntity>(ValidationMessages.AlreadyRegisteredFeminine(EntityNames.UserAccount));


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new UserAccountEntity(request.Email, hashedPassword);

            await _userAccountRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd($"{EntityNames.UserAccount} cadastrado com sucesso.", user);
        }
        catch (Exception ex)
        {
            return Result.CreateError<UserAccountEntity>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
