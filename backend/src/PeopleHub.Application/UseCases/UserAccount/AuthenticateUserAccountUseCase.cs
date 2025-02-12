using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Dtos.UserAccount;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;
using PeopleHub.Application.UseCases.Base;
using PeopleHub.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeopleHub.Application.UseCases.UserAccount;

public class AuthenticateUserAccountUseCase : BaseLoggingUseCase, IAuthenticateUserAccountUseCase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IConfiguration _configuration;

    public AuthenticateUserAccountUseCase(
        IUserAccountRepository userAccountRepository,
        IConfiguration configuration,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider) : base(auditLogService, httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _userAccountRepository = userAccountRepository;
        _configuration = configuration;
    }

    public async Task<ApiResponseDto<object>> ExecuteAsync(UserAccountLoginDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return await ResponseAsync<object>(
                    logAction: LogAction.VALIDATION_ERROR,
                    eventValue: request,
                    message: "Invalid email or password."
                );

            // JWT Configuration.
            var secretKey = _configuration["Jwt:Secret"];

            if (string.IsNullOrEmpty(secretKey))
                return ApiResponseDto<object>.Fail("SignIn", "JWT SecretKey is missing in configuration.", 500); // HTTP 500 - Internal Server Error

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return await ResponseAsync<object>(
                logAction: LogAction.LOGIN_SUCCESS,
                message: "Login successful.",
                data: jwtToken,
                userEmail: request.Email
            );
        }
        catch (Exception ex)
        {
            return await ResponseAsync<object>(logAction: LogAction.ERROR, message: ex.Message, userEmail: request.Email);
        }
    } 
}
