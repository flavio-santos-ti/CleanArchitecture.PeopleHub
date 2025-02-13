using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Dtos.IndividualPerson;
using PeopleHub.Application.Dtos.LegalPerson;
using PeopleHub.Application.Dtos.Person;
using PeopleHub.Application.Routers;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/persons")]
[ApiVersion("1.0")]
[Authorize]
public class PersonController : ControllerBase
{
    private readonly IPersonRouter _personRouter;

    public PersonController(IPersonRouter personService)
    {
        _personRouter = personService;
    }

    [HttpPost("individual")]
    [Authorize]
    public async Task<IActionResult> RegisterIndividual([FromBody] RegisterIndividualPersonRequestDto request)
    {
        var response = await _personRouter.RegisterIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("individual")]
    [Authorize]
    public async Task<IActionResult> UpdateIndividual([FromBody] UpdateIndividualPersonRequestDto request)
    {
        var response = await _personRouter.UpdateIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("individual")]
    [Authorize]
    public async Task<IActionResult> DeleteIndividual([FromBody] DeleteIndividualPersonDto request)
    {
        var response = await _personRouter.DeleteIndividualAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("individual/{cpf}")]
    [Authorize]
    public async Task<IActionResult> GetIndividualByCpf(string cpf)
    {
        var response = await _personRouter.GetIndividualByCpfAsync(cpf);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("legal")]
    public async Task<IActionResult> RegisterLegal([FromBody] RegisterLegalPersonRequestDto request)
    {
        var response = await _personRouter.RegisterLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("upload-photo")]
    [Authorize]
    public async Task<IActionResult> UploadPhoto([FromForm] UploadPersonPhotoDto request)
    {
        var response = await _personRouter.UploadPhotoAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("legal")]
    [Authorize]
    public async Task<IActionResult> UpdateLegal([FromBody] UpdateLegalPersonRequestDto request)
    {
        var response = await _personRouter.UpdateLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("legal")]
    [Authorize]
    public async Task<IActionResult> DeleteLegal([FromBody] DeleteLegalPersonDto request)
    {
        var response = await _personRouter.DeleteLegalAsync(request);

        return StatusCode(response.StatusCode, response);
    }
}