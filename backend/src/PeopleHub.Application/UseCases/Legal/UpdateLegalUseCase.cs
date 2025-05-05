using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Legal;

public class UpdateLegalUseCase : IUpdateLegalUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLegalUseCase(
        IPersonOldRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request)
    {
        try
        {
            var person = await _personRepository.GetByCnpjAsync(request.Cnpj);
            if (person == null)
                return Result.CreateNotFound<bool>("Legal Person not found.");

            person.UpdateLegalPerson(
                request.LegalName,
                request.TradeName,
                request.StateRegistration,
                request.MunicipalRegistration,
                new Address(request.Street, request.Number, request.Complement, request.City, request.State, request.ZipCode),
                new Phone(request.Phone),
                new Email(request.Email),
                request.LegalRepresentativeName,
                new Cpf(request.LegalRepresentativeCpf)
            );

            await _personRepository.UpdateAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateModify<bool>("Legal Person updated successfully.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
