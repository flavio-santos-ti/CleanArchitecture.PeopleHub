using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Photo;

public class UploadPersonPhotoUseCase : IUploadPhotoUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPersonPhotoUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(UploadPersonPhotoDto request)
    {
        try
        {
            if (request.Photo == null || request.Photo.Length == 0)
                return Result.CreateValidationError<bool>("Photo is required.");

            byte[] photoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await request.Photo.CopyToAsync(memoryStream);
                photoBytes = memoryStream.ToArray();
            }

            // Try to find the individual person by CPF
            var individualPerson = await _personRepository.GetByCpfAsync(request.Identifier);
            if (individualPerson != null)
            {
                individualPerson.UpdatePhoto(photoBytes);
                await _personRepository.UploadProfilePictureAsync(individualPerson);
                await _unitOfWork.CommitAsync();

                return Result.CreateUpload<bool>("Photo Legal Person successfully registered.");
            }

            // If it's not an individual person, try to find the legal entity by CNPJ
            var legalPerson = await _personRepository.GetByCnpjAsync(request.Identifier);
            if (legalPerson != null)
            {
                legalPerson.UpdatePhoto(photoBytes);
                await _personRepository.UpdateLegalPhotoAsync(legalPerson);

                return Result.CreateUpload<bool>("Photo Individual Person successfully registered.");
            }

            return Result.CreateNotFound<bool>("Person not found.");
        }
        catch (Exception ex)
        {
            return Result.CreateError<bool>($"An unexpected error occurred: {ex.Message}");
        }
    }
}