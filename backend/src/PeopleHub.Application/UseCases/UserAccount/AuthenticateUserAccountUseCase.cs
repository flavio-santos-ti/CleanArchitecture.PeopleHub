using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        IContextProvider contextProvider) : base(httpContextAccessor, authenticatedUserService, contextProvider)
    {
        _userAccountRepository = userAccountRepository;
        _configuration = configuration;
    }

    public async Task<Response<object>> ExecuteAsync(UserAccountLoginDto request)
    {
        try
        {
            var user = await _userAccountRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Result.CreateValidationError<object>("Invalid email or password.");

            // JWT Configuration.
            var secretKey = _configuration["Jwt:Secret"];

            if (string.IsNullOrEmpty(secretKey))
                return Result.CreateValidationError<object>("JWT SecretKey is missing in configuration.");

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

            return Result.CreateLoginSuccess<object>(jwtToken);
        }
        catch (Exception ex)
        {
            string msg = $"An unexpected error occurred: {ex.Message}"; 
            return Result.CreateError<object>(msg);
        }
    } 
}
