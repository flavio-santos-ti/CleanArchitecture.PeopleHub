using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IUserAccountService _userAccountService;

    public AuthController(IUserAccountService userAccountService)
    {
        _userAccountService = userAccountService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserAccountDto request)
    {
        var response = await _userAccountService.RegisterAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserAccountLoginDto request)
    {
        var response = await _userAccountService.AuthenticateAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserAccountDto request)
    {
        var response = await _userAccountService.UpdateAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserAccountDto request)
    {
        var response = await _userAccountService.DeleteAsync(request);

        if (!response.IsSuccess)
            return StatusCode(response.StatusCode, response);

        return StatusCode(response.StatusCode, response);
    }
}