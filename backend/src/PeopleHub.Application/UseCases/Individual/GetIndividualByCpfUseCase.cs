using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Individual;

public class GetIndividualByCpfUseCase : IGetIndividualByCpfUseCase
{
    private readonly IPersonRepository _personRepository;


    public GetIndividualByCpfUseCase(
        IPersonRepository personRepository, 
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
            return Result.CreateNotFound<IndividualPersonDto?>("Individual Person not found.");

        var personDto = new IndividualPersonDto(person);

        return Result.CreateGet<IndividualPersonDto?>("Individual person retrieved successfully.", personDto);
    }
}
