﻿using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Response;

namespace PeopleHub.Application.UseCases.Legal.Interfaces;

public interface IDeleteLegalUseCase
{
    Task<ApiResponseDto<bool>> ExecuteAsync(DeleteLegalPersonDto request);
}
