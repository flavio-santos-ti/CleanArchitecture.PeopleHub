using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Application.UseCases.Photo;

public class UploadPersonPhotoUseCase : BaseLoggingUseCase, IUploadPhotoUseCase
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPersonPhotoUseCase(
        IPersonRepository personRepository, 
        IUnitOfWork unitOfWork,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> ExecuteAsync(UploadPersonPhotoDto request)
    {
        try
        {
            if (request.Photo == null || request.Photo.Length == 0)
                return await ResponseAsync<bool>(
                    logAction: LogAction.VALIDATION_ERROR,
                    eventValue: request,
                    message: "Photo is required."
                );

            byte[] photoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await request.Photo.CopyToAsync(memoryStream);
                photoBytes = memoryStream.ToArray();
            }

            // Try to find the individual person by CPF
            var individualPerson = await _personRepository.GetIndividualByCpfAsync(request.Identifier);
            if (individualPerson != null)
            {
                individualPerson.UpdatePhoto(photoBytes);
                await _personRepository.UpdateIndividualPhotoAsync(individualPerson);
                await _unitOfWork.CommitAsync();
                return await ResponseAsync<bool>(
                    logAction: LogAction.CREATE_UPLOAD,
                    message: "Photo Legal Person successfully registered."
                );
            }

            // If it's not an individual person, try to find the legal entity by CNPJ
            var legalPerson = await _personRepository.GetLegalByCnpjAsync(request.Identifier);
            if (legalPerson != null)
            {
                legalPerson.UpdatePhoto(photoBytes);
                await _personRepository.UpdateLegalPhotoAsync(legalPerson);

                return await ResponseAsync<bool>(
                    logAction: LogAction.CREATE_UPLOAD,
                    message: "Photo Individual Person successfully registered."
                );
            }

            return await ResponseAsync<bool>(
                logAction: LogAction.NOT_FOUND,
                eventValue: request,
                message: "Person not found."
            );
        }
        catch (Exception ex)
        {
            return await ResponseAsync<bool>(logAction: LogAction.ERROR, message: ex.Message);
        }
    }
}