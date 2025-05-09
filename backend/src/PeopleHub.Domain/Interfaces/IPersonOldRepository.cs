﻿using PeopleHub.Domain.Entities;

namespace PeopleHub.Domain.Interfaces;

public interface IPersonOldRepository
{
    Task AddAsync(IndividualPersonEntity person);
    Task AddAsync(LegalPersonEntity person);
    Task<IndividualPersonEntity?> GetByCpfAsync(string cpf);
    Task<LegalPersonEntity?> GetByCnpjAsync(string cnpj);
    Task UpdateAsync(IndividualPersonEntity person);
    Task UploadCompanyLogoPhotoAsync(LegalPersonEntity person);
    Task UploadProfilePictureAsync(IndividualPersonEntity person);
    Task DeleteAsync(IndividualPersonEntity person);
    Task UpdateAsync(LegalPersonEntity person);
    Task DeleteAsync(LegalPersonEntity person);
}
