using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.Person;

public interface IPersonService
{
    Task<ApiResponseDto<bool>> RegisterIndividualAsync(RegisterIndividualPersonRequestDto request);
    Task<ApiResponseDto<bool>> RegisterLegalAsync(RegisterLegalPersonRequestDto request);
    Task<ApiResponseDto<IndividualPersonEntity?>> GetIndividualByCpfAsync(string cpf);
    Task<ApiResponseDto<bool>> UploadPhotoAsync(UploadPersonPhotoDto request);
    Task<ApiResponseDto<bool>> UpdateIndividualAsync(UpdateIndividualPersonRequestDto request);
    Task<ApiResponseDto<bool>> DeleteIndividualAsync(DeleteIndividualPersonDto request);
    Task<ApiResponseDto<bool>> UpdateLegalAsync(UpdateLegalPersonRequestDto request);
    Task<ApiResponseDto<bool>> DeleteLegalAsync(DeleteLegalPersonDto request);
}
