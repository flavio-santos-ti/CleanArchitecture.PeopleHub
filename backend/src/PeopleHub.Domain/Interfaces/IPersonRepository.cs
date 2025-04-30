using PeopleHub.Domain.Entities;

namespace PeopleHub.Domain.Interfaces;

public interface IPersonRepository
{
    Task AddAsync(IndividualPersonEntity person);
    Task AddAsync(LegalPersonEntity person);
    Task<IndividualPersonEntity?> GetByCpfAsync(string cpf);
    Task<LegalPersonEntity?> GetByCnpjAsync(string cnpj);
    Task UpdateAsync(IndividualPersonEntity person);
    Task UpdateLegalPhotoAsync(LegalPersonEntity person);
    Task UpdateIndividualPhotoAsync(IndividualPersonEntity person);
    Task DeleteIndividualAsync(IndividualPersonEntity person);
    Task UpdateLegalAsync(LegalPersonEntity person);
    Task DeleteLegalAsync(LegalPersonEntity person);
}
