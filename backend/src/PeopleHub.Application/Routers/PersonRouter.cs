using FDS.NetCore.ApiResponse.Models;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.UseCases.Individual.Interfaces;
using PeopleHub.Application.UseCases.Legal.Interfaces;
using PeopleHub.Application.UseCases.Photo;

namespace PeopleHub.Application.Routers;

public class PersonRouter : IPersonRouter
{
    private readonly IAddIndividualUseCase _addIndividualPersonUseCase;
    private readonly IRegisterLegalUseCase _registerLegalPersonUseCase;
    private readonly IGetIndividualByCpfUseCase _getIndividualPersonByCpfUseCase;
    private readonly IUploadPhotoUseCase _uploadPhotoUseCase;
    private readonly IUpdateIndividualUseCase _updateIndividualPersonUseCase;
    private readonly IDeleteIndividualUseCase _deleteIndividualPersonUseCase;
    private readonly IUpdateLegalUseCase _updateLegalPersonUseCase;
    private readonly IDeleteLegalUseCase _deleteLegalPersonUseCase;

    public PersonRouter(
        IAddIndividualUseCase addIndividualPersonUseCase,
        IRegisterLegalUseCase registerLegalPersonUseCase,
        IGetIndividualByCpfUseCase getIndividualPersonByCpfUseCase,
        IUploadPhotoUseCase uploadPhotoUseCase,
        IUpdateIndividualUseCase updateIndividualPersonUseCase,
        IDeleteIndividualUseCase deleteIndividualPersonUseCase,
        IUpdateLegalUseCase updateLegalPersonUseCase,
        IDeleteLegalUseCase deleteLegalPersonUseCase)
    {
        _addIndividualPersonUseCase = addIndividualPersonUseCase;
        _registerLegalPersonUseCase = registerLegalPersonUseCase;
        _getIndividualPersonByCpfUseCase = getIndividualPersonByCpfUseCase;
        _uploadPhotoUseCase = uploadPhotoUseCase;
        _updateIndividualPersonUseCase = updateIndividualPersonUseCase;
        _deleteIndividualPersonUseCase = deleteIndividualPersonUseCase;
        _updateLegalPersonUseCase = updateLegalPersonUseCase;
        _deleteLegalPersonUseCase = deleteLegalPersonUseCase;
    }

    public async Task<Response<bool>> AddIndividualAsync(RegisterIndividualPersonRequestDto request)
    {
        return await _addIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> RegisterLegalAsync(RegisterLegalPersonRequestDto request)
    {
        return await _registerLegalPersonUseCase.ExecuteAsync(request);
    }

    public async Task<Response<IndividualPersonDto?>> GetIndividualByCpfAsync(string cpf)
    {
        return await _getIndividualPersonByCpfUseCase.ExecuteAsync(cpf);
    }

    public async Task<Response<bool>> UploadPhotoAsync(UploadPersonPhotoDto request)
    {
        return await _uploadPhotoUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> UpdateIndividualAsync(UpdateIndividualPersonRequestDto request)
    {
        return await _updateIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> DeleteIndividualAsync(DeleteIndividualPersonDto request)
    {
        return await _deleteIndividualPersonUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> UpdateLegalAsync(UpdateLegalPersonRequestDto request)
    {
        return await _updateLegalPersonUseCase.ExecuteAsync(request);
    }

    public async Task<Response<bool>> DeleteLegalAsync(DeleteLegalPersonDto request)
    {
        return await _deleteLegalPersonUseCase.ExecuteAsync(request);
    }
}
