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

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost("individual")]
    [Authorize]
    public async Task<IActionResult> RegisterIndividual([FromBody] AddIndividualPersonRequestDto request)
    {
        var response = await _personService.AddIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("individual")]
    [Authorize]
    public async Task<IActionResult> UpdateIndividual([FromBody] UpdateIndividualPersonRequestDto request)
    {
        var response = await _personService.UpdateIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("individual")]
    [Authorize]
    public async Task<IActionResult> DeleteIndividual([FromBody] DeleteIndividualPersonDto request)
    {
        var response = await _personService.DeleteIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("individual/{cpf}")]
    [Authorize]
    public async Task<IActionResult> GetIndividualByCpf(string cpf)
    {
        var response = await _personService.GetIndividualByCpfAsync(cpf);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("legal")]
    public async Task<IActionResult> RegisterLegal([FromBody] AddLegalPersonRequestDto request)
    {
        var response = await _personService.AddLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("upload-photo")]
    [Authorize]
    public async Task<IActionResult> UploadPhoto([FromForm] UploadPersonPhotoDto request)
    {
        var response = await _personService.UploadPhotoAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("legal")]
    [Authorize]
    public async Task<IActionResult> UpdateLegal([FromBody] UpdateLegalPersonRequestDto request)
    {
        var response = await _personService.UpdateLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("legal")]
    [Authorize]
    public async Task<IActionResult> DeleteLegal([FromBody] DeleteLegalPersonDto request)
    {
        var response = await _personService.DeleteLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }
}