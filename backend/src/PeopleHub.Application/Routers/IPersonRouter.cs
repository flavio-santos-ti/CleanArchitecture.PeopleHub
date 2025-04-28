using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;

namespace PeopleHub.Application.Routers;

public interface IPersonRouter
{
    Task<Response<bool>> AddIndividualAsync(RegisterIndividualPersonRequestDto request);
    Task<Response<bool>> AddLegalAsync(RegisterLegalPersonRequestDto request);
    Task<Response<IndividualPersonDto?>> GetIndividualByCpfAsync(string cpf);
    Task<Response<bool>> UploadPhotoAsync(UploadPersonPhotoDto request);
    Task<Response<bool>> UpdateIndividualAsync(UpdateIndividualPersonRequestDto request);
    Task<Response<bool>> DeleteIndividualAsync(DeleteIndividualPersonDto request);
    Task<Response<bool>> UpdateLegalAsync(UpdateLegalPersonRequestDto request);
    Task<Response<bool>> DeleteLegalAsync(DeleteLegalPersonDto request);
}
