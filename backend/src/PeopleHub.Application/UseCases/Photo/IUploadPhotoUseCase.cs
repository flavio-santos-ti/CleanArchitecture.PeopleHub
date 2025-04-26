using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.Person;

namespace PeopleHub.Application.UseCases.Photo;

public interface IUploadPhotoUseCase
{
    Task<Response<bool>> ExecuteAsync(UploadPersonPhotoDto request);
}