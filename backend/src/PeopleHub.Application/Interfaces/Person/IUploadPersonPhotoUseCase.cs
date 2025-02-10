using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.Interfaces.Person;

public interface IUploadPersonPhotoUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UploadPersonPhotoDto request);
}