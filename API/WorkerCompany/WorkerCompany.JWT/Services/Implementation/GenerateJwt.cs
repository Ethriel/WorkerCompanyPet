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
using WorkerCompany.Authentication.Models;
using WorkerCompany.JWT.Services.Abstraction;

namespace WorkerCompany.JWT.Services.Implementation
{
    public class GenerateJwt : IGenerateJwt
    {
        private readonly SymmetricSecurityKey key;
        private readonly UserManager<AppUser> userManager;

        public GenerateJwt(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSecret"]));
            this.userManager = userManager;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);
            var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            claims.AddRange(rolesClaims);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
