using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Legal;

public class AddLegalUseCase : IAddLegalUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddLegalUseCase(
        IPersonOldRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(AddLegalPersonRequestDto request)
    {
        try
        {
            var existingPerson = await _personRepository.GetByCnpjAsync(request.Cnpj);

            if (existingPerson != null)
                return Result.CreateValidationError<bool>("Person jurídica já cadastrada.");

            var cnpj = new Cnpj(request.Cnpj);
            var cpf = new Cpf(request.LegalRepresentativeCpf);

            var person = new LegalPersonEntity(
                request.LegalName,
                request.TradeName,
                cnpj,
                request.StateRegistration,
                request.MunicipalRegistration,
                request.LegalRepresentativeName,
                cpf);

            await _personRepository.AddAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd<bool>("Pessoa jurídica cadastrada com sucesso.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }

    }
}