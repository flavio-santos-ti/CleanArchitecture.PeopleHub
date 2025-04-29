using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleHub.Application.Configurations;
using PeopleHub.Infrastructure.Configurations;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PeopleHub.Configuration.Configurations;

public static class Configuration
{
    public static IServiceCollection AddAppConfigServices(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Configuration
        services.AddJwtAuthenticationConfiguration(configuration);

        // Configures the Application Layer
        services.AddApplicationServices(configuration);

        // Configures the Infrastructure Layer
        services.AddInfrastructureServices(configuration);

        return services;
    }

    public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JwtConfiguration(configuration);
        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        services.AddSingleton(jwtConfig);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}