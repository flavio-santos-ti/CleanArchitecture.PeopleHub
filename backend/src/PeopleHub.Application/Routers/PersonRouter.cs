using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Person;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Application.UseCases.Legal.Interfaces;

namespace PeopleHub.Application.Routers;

public class PersonRouter : IPersonRouter
{
    private readonly IRegisterIndividualUseCase _registerIndividualPersonUseCase;
    private readonly IRegisterLegalUseCase _registerLegalPersonUseCase;
    private readonly IGetIndividualByCpfUseCase _getIndividualPersonByCpfUseCase;
    private readonly IUploadPersonPhotoUseCase _uploadPhotoUseCase;
    private readonly IUpdateIndividualUseCase _updateIndividualPersonUseCase;
    private readonly IDeleteIndividualUseCase _deleteIndividualPersonUseCase;
    private readonly IUpdateLegalUseCase _updateLegalPersonUseCase;
    private readonly IDeleteLegalUseCase _deleteLegalPersonUseCase;

    public PersonRouter(
        IRegisterIndividualUseCase registerIndividualPersonUseCase,
        IRegisterLegalUseCase registerLegalPersonUseCase,
        IGetIndividualByCpfUseCase getIndividualPersonByCpfUseCase,
        IUploadPersonPhotoUseCase uploadPhotoUseCase,
        IUpdateIndividualUseCase updateIndividualPersonUseCase,
        IDeleteIndividualUseCase deleteIndividualPersonUseCase,
        IUpdateLegalUseCase updateLegalPersonUseCase,
        IDeleteLegalUseCase deleteLegalPersonUseCase)
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

    public async Task<ApiResponseDto<IndividualPersonDto?>> GetIndividualByCpfAsync(string cpf)
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
