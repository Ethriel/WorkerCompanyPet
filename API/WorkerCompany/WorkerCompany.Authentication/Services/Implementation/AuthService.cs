using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.Authentication.Models.Auth;
using WorkerCompany.Authentication.Models.Responses;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.Authentication.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateJwt generateJwt;
        private readonly IEventService events;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            IGenerateJwt generateJwt,
            IEventService events)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.generateJwt = generateJwt;
            this.events = events;
        }
        public async Task<AuthResponse> SignIn(SignInModel signInModel, ModelStateDictionary modelState, HttpContext httpContext)
        {
            var response = default(AuthResponse);
            var errorMessage = "Sign in error";

            if (!modelState.IsValid)
            {
                response = AuthResponseError.FromModelStateErrors(modelState, errorMessage);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(signInModel.UserName);
                var errors = default(IEnumerable<string>);

                if (user.Equals(null))
                {
                    errors = GetErrorsFromParams($"User {signInModel.UserName} was not found");
                    response = AuthResponseError.GetNotFoundError(errors, errorMessage);
                }
                else
                {
                    var result = await signInManager.PasswordSignInAsync(user, signInModel.Password, false, false);
                    if (!result.Succeeded)
                    {
                        errors = GetErrorsFromParams($"Incorrect password");
                        response = AuthResponseError.GetBadRequestError(errors, errorMessage);
                    }
                    else
                    {
                        await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

                        var authProps = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                        };
                        var issuer = new IdentityServerUser(user.Id)
                        {
                            DisplayName = user.DisplayName
                        };

                        await httpContext.SignInAsync(issuer, authProps);

                        var token = await generateJwt.CreateToken(user);
                        var authModel = await AuthModel.FromAppUser(user, userManager, token);
                        response = new AuthResponseOk(authModel, "Sign in success");
                    }
                }
            }

            GC.Collect();
            return response;
        }

        public async Task<AuthResponse> SignUp(SignUpModel signUpModel, ModelStateDictionary modelState, HttpContext httpContext)
        {
            var response = default(AuthResponse);
            var errorMessage = "Sign up error";

            if (!modelState.IsValid)
            {
                response = AuthResponseError.FromModelStateErrors(modelState, errorMessage);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(signUpModel.UserName);

                if (!user.Equals(null))
                {
                    var errors = GetErrorsFromParams($"User {signUpModel.UserName} already exists");
                    response = AuthResponseError.GetBadRequestError(errors, errorMessage);
                }
                else
                {
                    user = new AppUser(signUpModel.DisplayName, signUpModel.UserName, signUpModel.WorkerId);
                    var result = await userManager.CreateAsync(user, signUpModel.Password);

                    if (!result.Succeeded)
                    {
                        response = AuthResponseError.FromIdentityErrors(result.Errors, errorMessage);
                    }
                    else
                    {
                        result = await userManager.AddToRoleAsync(user, Roles.User);
                        if (!result.Succeeded)
                        {
                            response = AuthResponseError.FromIdentityErrors(result.Errors, errorMessage);
                        }
                        else
                        {
                            response = new AuthResponseOk("Sign up success. Sign in with your credentials");
                        }
                    }
                }
            }

            GC.Collect();
            return response;
        }

        public async Task<AuthResponse> SignOut(HttpContext httpContext, ClaimsPrincipal user)
        {
            var userId = user.GetSubjectId();
            var displayName = user.GetDisplayName();
            if (httpContext.Request.Cookies.Any())
            {
                var cookies = httpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in cookies)
                {
                    httpContext.Response.Cookies.Delete(cookie.Key);
                }
            }

            await httpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);

            await signInManager.SignOutAsync();
            await events.RaiseAsync(new UserLogoutSuccessEvent(userId, displayName));

            var response = new AuthResponseOk("Sign out success");

            GC.Collect();
            return response;
        }

        private IEnumerable<string> GetErrorsFromParams(params string[] @params)
        {
            return @params.AsEnumerable();
        }
    }
}
