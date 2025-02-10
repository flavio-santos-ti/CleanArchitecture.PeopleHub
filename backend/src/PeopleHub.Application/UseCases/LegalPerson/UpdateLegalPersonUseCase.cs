using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.LegalPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.LegalPerson;

public class UpdateLegalPersonUseCase : BaseAuditableUseCase, IUpdateLegalPersonUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLegalPersonUseCase(
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

    public async Task<ApiResponseDto<bool>> ExecuteAsync(UpdateLegalPersonRequestDto request)
    {
        try
        {
            var person = await _personRepository.GetLegalByCnpjAsync(request.Cnpj);
            if (person == null)
                return await AuditNotFoundErrorAsync<bool>(
                    eventValue: request,
                    message: "Legal Person not found."
                );

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

            return await UpdateSuccessWithAudit<bool>(
                eventValue: request,
                oldValue: person,
                message: "Legal Person updated successfully."
            );
        }
        catch (Exception ex)
        {
            return await AuditExceptionAsync<bool>(message: ex.Message);
        }
    }
}
