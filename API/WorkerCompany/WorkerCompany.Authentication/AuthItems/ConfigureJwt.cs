using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WorkerCompany.Authentication.AuthItems
{
    public static class ConfigureJwt
    {
        public static IServiceCollection Configure(IServiceCollection services, string secret, string issuer, string audience)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            var symmetricKey = new SymmetricSecurityKey(key);
            
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = symmetricKey,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
