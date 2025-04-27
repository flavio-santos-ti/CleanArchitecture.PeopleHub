using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Legal;

public class RegisterLegalUseCase : IRegisterLegalUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterLegalUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuditLogService auditLogService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(RegisterLegalPersonRequestDto request)
    {
        try
        {
            var existingPerson = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);

            if (existingPerson != null)
                return Result.CreateValidationError<bool>("Person already exists.");

            var address = new Address(request.Street, request.Number, request.Complement, request.City, request.State, request.ZipCode);
            var phone = new Phone(request.Phone);
            var email = new Email(request.Email);
            var cnpj = new Cnpj(request.Cnpj);
            var cpf = new Cpf(request.LegalRepresentativeCpf);

            var person = new LegalPersonEntity(
                request.LegalName,
                request.TradeName,
                cnpj,
                request.StateRegistration,
                request.MunicipalRegistration,
                address,
                phone,
                email,
                request.LegalRepresentativeName,
                cpf);

            await _personRepository.AddAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd<bool>("Legal Person successfully registered.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }

    }
}