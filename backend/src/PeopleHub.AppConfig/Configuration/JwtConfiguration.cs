﻿using Microsoft.Extensions.Configuration;

namespace PeopleHub.AppConfig.Configuration
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;

        public JwtConfiguration(IConfiguration configuration)
        {
            configuration.GetSection("Jwt").Bind(this);
        }
    }
}
