using FDS.NetCore.ApiResponse.Models;
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

public class UpdateIndividualUseCase : IUpdateIndividualUseCase
{
    private readonly IPersonOldRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateIndividualUseCase(
        IPersonOldRepository personRepository, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) 
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(UpdateIndividualPersonRequestDto request)
    {
        try
        {
            var person = await _personRepository.GetByCpfAsync(request.Cpf);
            if (person == null)
                return Result.CreateNotFound<bool>(NotFoundMessages.Feminine(EntityNames.IndividualPerson));

            var cpf = new Cpf(request.Cpf);

            person.Update(
                request.FullName,
                request.BirthDate,
                request.Gender
            );

            await _personRepository.UpdateAsync(person);
            await _unitOfWork.CommitAsync();

            return Result.CreateModify<bool>($"{EntityNames.IndividualPerson} atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>(string.Format(SystemMessages.UnexpectedError, ex.Message));
        }
    }
}
