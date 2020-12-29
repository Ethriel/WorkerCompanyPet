using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.Authentication.Models;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly WorkerCompanyPetContext context;
        private readonly IGenerateJwt generateJwt;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(WorkerCompanyPetContext context, IGenerateJwt generateJwt, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.generateJwt = generateJwt;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var mse in ModelState.Values)
                {
                    foreach (var error in mse.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(errors);
            }

            var user = await userManager.FindByEmailAsync(signInModel.UserName);

            var token = await generateJwt.CreateToken(user);

            return Ok(new { token = token });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("secret")]
        public IActionResult Hello()
        {
            return Ok("Hello, authorized");
        }
    }
}
