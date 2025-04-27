using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Individual;

public class RegisterIndividualUseCase : BaseLoggingUseCase, IRegisterIndividualUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterIndividualUseCase(
        IPersonRepository personRepository,
        IUnitOfWork unitOfWork,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base()
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(RegisterIndividualPersonRequestDto request)
    { 
        try
        {
            var existingPerson = await _personRepository.GetIndividualByCpfAsync(request.Cpf);

            if (existingPerson != null)
                return Result.CreateValidationError<bool>("Person already exists.");

            var address = new Address(request.Street, request.Number, request.Complement, request.City, request.State, request.ZipCode);
            var phone = new Phone(request.Phone);
            var email = new Email(request.Email);
            var cpf = new Cpf(request.Cpf);

            var person = new IndividualPersonEntity(
                request.FullName,
                cpf,
                request.BirthDate,
                request.Gender,
                address,
                phone,
                email
            );

            await _personRepository.AddAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd<bool>("User registered successfully.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
