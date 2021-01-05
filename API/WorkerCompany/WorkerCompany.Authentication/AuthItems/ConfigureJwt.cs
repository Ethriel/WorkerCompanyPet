using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WorkerCompany.Authentication.AuthItems
{
    public static class ConfigureJwt
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, string authShceme = "Bearer")
        {
            var secret = configuration["JwtSecret"];
            var issuer = configuration["JwtIssuer"];
            var audience = configuration["JwtAudience"];
            var authProviderKey = configuration["AuthScheme"];
            var key = Encoding.ASCII.GetBytes(secret);
            var symmetricKey = new SymmetricSecurityKey(key);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = authShceme;
                options.DefaultAuthenticateScheme = authShceme;
                options.DefaultChallengeScheme = authShceme;
            }).AddJwtBearer(authShceme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    IssuerSigningKey = symmetricKey,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
