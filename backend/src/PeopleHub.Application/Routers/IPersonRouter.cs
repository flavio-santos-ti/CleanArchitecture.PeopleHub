using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Routers;

public interface IPersonRouter
{
    Task<ApiResponseDto<bool>> RegisterIndividualAsync(RegisterIndividualPersonRequestDto request);
    Task<ApiResponseDto<bool>> RegisterLegalAsync(RegisterLegalPersonRequestDto request);
    Task<ApiResponseDto<IndividualPersonDto?>> GetIndividualByCpfAsync(string cpf);
    Task<ApiResponseDto<bool>> UploadPhotoAsync(UploadPersonPhotoDto request);
    Task<ApiResponseDto<bool>> UpdateIndividualAsync(UpdateIndividualPersonRequestDto request);
    Task<ApiResponseDto<bool>> DeleteIndividualAsync(DeleteIndividualPersonDto request);
    Task<ApiResponseDto<bool>> UpdateLegalAsync(UpdateLegalPersonRequestDto request);
    Task<ApiResponseDto<bool>> DeleteLegalAsync(DeleteLegalPersonDto request);
}
