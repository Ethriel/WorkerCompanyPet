using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WorkerCompany.Authentication.Models.Auth;
using WorkerCompany.Authentication.Models.Responses;

namespace WorkerCompany.Authentication.Services.Abstraction
{
    public interface IAuthService
    {
        Task<AuthResponse> SignIn(SignInModel signInModel, ModelStateDictionary modelState);
        Task<AuthResponse> SignUp(SignUpModel signUpModel, ModelStateDictionary modelState);
        Task<AuthResponse> SignOut(SignOutModel signOutModel);
    }
}
