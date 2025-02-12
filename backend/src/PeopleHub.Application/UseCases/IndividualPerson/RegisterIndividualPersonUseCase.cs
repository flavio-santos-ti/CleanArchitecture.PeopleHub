using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.IndividualPerson;

public class RegisterIndividualPersonUseCase : BaseLoggingUseCase, IRegisterIndividualPersonUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterIndividualPersonUseCase(
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

    public async Task<ApiResponseDto<bool>> ExecuteAsync(RegisterIndividualPersonRequestDto request)
    { 
        try
        {
            var existingPerson = await _personRepository.GetIndividualByCpfAsync(request.Cpf);

            if (existingPerson != null)
                return await ResponseAsync<bool>(
                    logAction: LogAction.VALIDATION_ERROR,
                    eventValue: request,
                    message: "Person already exists."
                );



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

            return await ResponseAsync<bool>(
                logAction: LogAction.CREATE,
                eventValue: person,
                message: "Individual Person successfully registered."
            );
        }
        catch (Exception ex)
        {
            return await ResponseAsync<bool>(logAction: LogAction.ERROR, message: ex.Message);
        }
    }
}
