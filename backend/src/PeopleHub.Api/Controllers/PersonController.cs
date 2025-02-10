using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Interfaces.Person;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/persons")]
[ApiVersion("1.0")]
[Authorize]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpPost("individual")]
    [Authorize]
    public async Task<IActionResult> RegisterIndividual([FromBody] RegisterIndividualPersonRequestDto request)
    {
        _logger.LogInformation("Registering individual person with CPF: {Cpf}", request.Cpf);
        var response = await _personService.RegisterIndividualAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("individual")]
    [Authorize]
    public async Task<IActionResult> UpdateIndividual([FromBody] UpdateIndividualPersonRequestDto request)
    {
        _logger.LogInformation("Updating individual person with CPF: {Cpf}", request.Cpf);
        var response = await _personService.UpdateIndividualAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("individual")]
    [Authorize]
    public async Task<IActionResult> DeleteIndividual([FromBody] DeleteIndividualPersonDto request)
    {
        _logger.LogInformation("Deleting individual person with CPF: {Cpf}", request.Cpf);
        var response = await _personService.DeleteIndividualAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("individual/{cpf}")]
    [Authorize]
    public async Task<IActionResult> GetIndividualByCpf(string cpf)
    {
        _logger.LogInformation("Fetching individual person by CPF: {Cpf}", cpf);
        var person = await _personService.GetIndividualByCpfAsync(cpf);
        return person == null ? NotFound() : Ok(person);
    }

    [HttpPost("legal")]
    public async Task<IActionResult> RegisterLegal([FromBody] RegisterLegalPersonRequestDto request)
    {
        _logger.LogInformation("Registering legal person with CNPJ: {Cnpj}", request.Cnpj);
        var response = await _personService.RegisterLegalAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("upload-photo")]
    [Authorize]
    public async Task<IActionResult> UploadPhoto([FromForm] UploadPersonPhotoDto request)
    {
        _logger.LogInformation("Uploading photo for person with ID: {PersonId}", request.Identifier);

        var response = await _personService.UploadPhotoAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("legal")]
    [Authorize]
    public async Task<IActionResult> UpdateLegal([FromBody] UpdateLegalPersonRequestDto request)
    {
        _logger.LogInformation("Updating legal person with CNPJ: {Cnpj}", request.Cnpj);
        var response = await _personService.UpdateLegalAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("legal")]
    [Authorize]
    public async Task<IActionResult> DeleteLegal([FromBody] DeleteLegalPersonDto request)
    {
        _logger.LogInformation("Deleting legal person with CNPJ: {Cnpj}", request.Cnpj);
        var response = await _personService.DeleteLegalAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }
}