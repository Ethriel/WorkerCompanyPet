using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkerCompany.Authentication.Models.Auth;
using WorkerCompany.Authentication.Models.Responses;

namespace WorkerCompany.Authentication.Services.Abstraction
{
    public interface IAuthService
    {
        Task<AuthResponse> SignIn(SignInModel signInModel, ModelStateDictionary modelState, HttpContext httpContext);
        Task<AuthResponse> SignUp(SignUpModel signUpModel, ModelStateDictionary modelState, HttpContext httpContext);
        Task<AuthResponse> SignOut(HttpContext httpContext, ClaimsPrincipal user);
    }
}
