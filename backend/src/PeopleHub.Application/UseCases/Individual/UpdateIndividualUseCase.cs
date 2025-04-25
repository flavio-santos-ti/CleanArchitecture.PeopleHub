using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Domain.Interfaces;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.UseCases.Individual;

public class UpdateIndividualUseCase : BaseLoggingUseCase, IUpdateIndividualUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateIndividualUseCase(
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

    public async Task<Response<bool>> ExecuteAsync(UpdateIndividualPersonRequestDto request)
    {
        try
        {
            var person = await _personRepository.GetIndividualByCpfAsync(request.Cpf);
            if (person == null)
                return Result.CreateNotFound<bool>("Individual Person not found.");

            var address = new Address(request.Street, request.Number, request.Complement, request.City, request.State, request.ZipCode);
            var phone = new Phone(request.Phone);
            var email = new Email(request.Email);
            var cpf = new Cpf(request.Cpf);

            person.UpdateIndividualPerson(
                request.FullName,
                request.BirthDate,
                request.Gender,
                address,
                phone,
                email
            );

            await _personRepository.UpdateIndividualAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateModify<bool>("Individual Person updated successfully.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}
