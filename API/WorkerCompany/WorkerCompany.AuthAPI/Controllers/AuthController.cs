using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.AuthAPI.Extensions;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.Authentication.Models.Auth;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly WorkerCompanyPetContext context;
        private readonly IAuthService authService;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(WorkerCompanyPetContext context, IAuthService authService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.authService = authService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signInModel)
        {
            var authResponse = await authService.SignIn(signInModel, ModelState, HttpContext);
            return this.GetResponseResult(authResponse);
        }

        [AllowAnonymous]
        [HttpGet("sign-out")]
        public async Task<IActionResult> SignOutAsync()
        {
            var localUser = User;

            var authResponse = await authService.SignOut(HttpContext, User);
            return this.GetResponseResult(authResponse);
        }

        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("secret")]
        public IActionResult Hello()
        {
            return Ok("Hello, authorized");
        }
    }
}
