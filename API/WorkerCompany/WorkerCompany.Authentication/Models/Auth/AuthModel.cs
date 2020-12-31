using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.Authentication.Models.Auth
{
    public class AuthModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public AuthModel()
        {

        }
        public AuthModel(string id, string token, string displayName, string userName, IEnumerable<string> roles)
        {
            Id = id;
            Token = token;
            DisplayName = displayName;
            UserName = userName;
            Roles = roles;
        }

        public async static Task<AuthModel> FromAppUser(AppUser user, UserManager<AppUser> userManager, string token)
        {
            var roles = await userManager.GetRolesAsync(user);
            var model = new AuthModel(user.Id, token, user.DisplayName, user.UserName, roles);
            return model;
        }
    }
}
