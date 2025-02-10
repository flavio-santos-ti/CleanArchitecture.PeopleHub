using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.IndividualPerson;

public interface IGetIndividualPersonByCpfUseCase
{
    Task<IndividualPersonEntity?> ExecuteAsync(string cpf);
}
