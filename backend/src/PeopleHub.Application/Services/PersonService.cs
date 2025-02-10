using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.IndividualPerson;
using PeopleHub.Application.Interfaces.LegalPerson;
using PeopleHub.Application.Interfaces.Person;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Services;

public class PersonService : IPersonService
{
    private readonly IRegisterIndividualPersonUseCase _registerIndividualPersonUseCase;
    private readonly IRegisterLegalPersonUseCase _registerLegalPersonUseCase;
    private readonly IGetIndividualPersonByCpfUseCase _getIndividualPersonByCpfUseCase;
    private readonly IUploadPersonPhotoUseCase _uploadPhotoUseCase;
    private readonly IUpdateIndividualPersonUseCase _updateIndividualPersonUseCase;
    private readonly IDeleteIndividualPersonUseCase _deleteIndividualPersonUseCase;
    private readonly IUpdateLegalPersonUseCase _updateLegalPersonUseCase;
    private readonly IDeleteLegalPersonUseCase _deleteLegalPersonUseCase;

    public PersonService(
        IRegisterIndividualPersonUseCase registerIndividualPersonUseCase,
        IRegisterLegalPersonUseCase registerLegalPersonUseCase,
        IGetIndividualPersonByCpfUseCase getIndividualPersonByCpfUseCase,
        IUploadPersonPhotoUseCase uploadPhotoUseCase,
        IUpdateIndividualPersonUseCase updateIndividualPersonUseCase,
        IDeleteIndividualPersonUseCase deleteIndividualPersonUseCase,
        IUpdateLegalPersonUseCase updateLegalPersonUseCase,
        IDeleteLegalPersonUseCase deleteLegalPersonUseCase)
    {
        _registerIndividualPersonUseCase = registerIndividualPersonUseCase;
        _registerLegalPersonUseCase = registerLegalPersonUseCase;
        _getIndividualPersonByCpfUseCase = getIndividualPersonByCpfUseCase;
        _uploadPhotoUseCase = uploadPhotoUseCase;
        _updateIndividualPersonUseCase = updateIndividualPersonUseCase;
        _deleteIndividualPersonUseCase = deleteIndividualPersonUseCase;
        _updateLegalPersonUseCase = updateLegalPersonUseCase;
        _deleteLegalPersonUseCase = deleteLegalPersonUseCase;
    }

    public async Task<ApiResponseDto<bool>> RegisterIndividualAsync(RegisterIndividualPersonRequestDto request)
    {
        return await _registerIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> RegisterLegalAsync(RegisterLegalPersonRequestDto request)
    {
        return await _registerLegalPersonUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<IndividualPersonEntity?>> GetIndividualByCpfAsync(string cpf)
    {
        return await _getIndividualPersonByCpfUseCase.ExecuteAsync(cpf);
    }

    public async Task<ApiResponseDto<bool>> UploadPhotoAsync(UploadPersonPhotoDto request)
    {
        return await _uploadPhotoUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> UpdateIndividualAsync(UpdateIndividualPersonRequestDto request)
    {
        return await _updateIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> DeleteIndividualAsync(DeleteIndividualPersonDto request)
    {
        return await _deleteIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> UpdateLegalAsync(UpdateLegalPersonRequestDto request)
    {
        return await _updateLegalPersonUseCase.ExecuteAsync(request);
    }

    public async Task<ApiResponseDto<bool>> DeleteLegalAsync(DeleteLegalPersonDto request)
    {
        return await _deleteLegalPersonUseCase.ExecuteAsync(request);
    }
}
