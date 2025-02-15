﻿using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Legal;

public class RegisterLegalUseCase : BaseLoggingUseCase, IRegisterLegalUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterLegalUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IAuditLogService auditLogService, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<bool>> ExecuteAsync(RegisterLegalPersonRequestDto request)
    {
        try
        {
            var existingPerson = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);

            if (existingPerson != null)
                return await ResponseAsync<bool>(
                    logAction: LogAction.VALIDATION_ERROR,
                    eventValue: request,
                    message: "Person already exists."
                );



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

            return await ResponseAsync<bool>(
                logAction: LogAction.CREATE,
                eventValue: person,
                message: "Legal Person successfully registered."
            );
        }
        catch (Exception ex)
        {
            return await ResponseAsync<bool>(logAction: LogAction.ERROR, message: ex.Message);
        }

    }
}