using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.Authentication.Services.Implementation
{
    public class GenerateJwt : IGenerateJwt
    {
        private readonly SymmetricSecurityKey key;
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userManager;
        public GenerateJwt(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            var secretBytes = Encoding.ASCII.GetBytes(configuration["JwtSecret"]);
            key = new SymmetricSecurityKey(secretBytes);
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var now = DateTime.Now;
            var totalSeconds = now.Hour * 60 * 60 + now.Minute * 60 + now.Second;
            var authTime = totalSeconds.ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.IdentityProvider, "local"),
                new Claim(JwtClaimTypes.AuthenticationMethod, "pwd"),
                new Claim(JwtClaimTypes.AuthenticationTime, authTime),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);
            var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            claims.AddRange(rolesClaims);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = configuration["JwtIssuer"];

            var audience = configuration["JwtAudience"];

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddDays(7),
            //    SigningCredentials = credentials,
            //    Issuer = issuer,
            //    Audience = audience
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: DateTime.Now.AddDays(7), signingCredentials: credentials);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
