using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Photo;

public interface IUploadPhotoUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(UploadPersonPhotoDto request);
}