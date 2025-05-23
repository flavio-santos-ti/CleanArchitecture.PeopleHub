﻿using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Individual;

public class AddIndividualUseCase : IAddIndividualUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddIndividualUseCase(
        IPersonOldRepository personRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) 
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(AddIndividualPersonRequestDto request)
    { 
        try
        {
            var existingPerson = await _personRepository.GetByCpfAsync(request.Cpf);

            if (existingPerson != null)
                return Result.CreateValidationError<bool>(ValidationMessages.AlreadyRegisteredFeminine(EntityNames.IndividualPerson));

            var cpf = new Cpf(request.Cpf);

            var person = new IndividualPersonEntity(
                request.FullName,
                cpf,
                request.BirthDate,
                request.Gender
            );

            await _personRepository.AddAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateAdd<bool>(SuccessMessages.CreatedFeminine(EntityNames.IndividualPerson));
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
