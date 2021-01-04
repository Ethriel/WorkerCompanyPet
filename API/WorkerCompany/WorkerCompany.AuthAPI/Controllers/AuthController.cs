using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkerCompany.AuthAPI.Extensions;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.Authentication.Models.Auth;
using WorkerCompany.Authentication.Services.Abstraction;

namespace WorkerCompany.AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            var authResponse = await authService.SignIn(signInModel, ModelState);
            return this.GetResponseResult(authResponse);
        }

        [AllowAnonymous]
        [HttpGet("sign-out")]
        public async Task<IActionResult> SignOutAsync([FromBody] SignOutModel signOutModel)
        {
            var authResponse = await authService.SignOut(signOutModel);
            return this.GetResponseResult(authResponse);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("secret")]
        public IActionResult Hello()
        {
            return Ok("Hello, authorized");
        }
    }
}
