using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Legal;

public class UpdateLegalUseCase : BaseLoggingUseCase, IUpdateLegalUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLegalUseCase(
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

    public async Task<Response<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request)
    {
        try
        {
            var person = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);
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

            await _personRepository.UpdateLegalAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateModify<bool>("Legal Person updated successfully.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
