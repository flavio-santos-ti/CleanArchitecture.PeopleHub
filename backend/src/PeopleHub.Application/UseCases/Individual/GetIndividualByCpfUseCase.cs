﻿using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.Messages;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Individual;

public class GetIndividualByCpfUseCase : IGetIndividualByCpfUseCase
{
    private readonly IPersonOldRepository _personRepository;


    public GetIndividualByCpfUseCase(
        IPersonOldRepository personRepository, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) 
    {
        _personRepository = personRepository;
    }

    public async Task<Response<IndividualPersonDto?>> ExecuteAsync(string cpf)
    {
        var cleanedCpf = Cpf.Clean(cpf);
        var validationError = Cpf.Validate(cleanedCpf);

        if (validationError != null)
            return Result.CreateValidationError<IndividualPersonDto?>(validationError);

        var person = await _personRepository.GetByCpfAsync(cleanedCpf);

        if (person == null)
            return Result.CreateNotFound<IndividualPersonDto?>(NotFoundMessages.Feminine(EntityNames.IndividualPerson));

        var personDto = new IndividualPersonDto(person);

        return Result.CreateGet<IndividualPersonDto?>($"{EntityNames.IndividualPerson} retornada com sucesso.", personDto);
    }
}
