﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateJwt generateJwt;
        private readonly DbContext context;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IGenerateJwt generateJwt, DbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.generateJwt = generateJwt;
            this.context = context;
        }
        public async Task<AuthResponse> SignIn(SignInModel signInModel, ModelStateDictionary modelState)
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

                if (user == null)
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
                        var token = await generateJwt.CreateToken(user);
                        var authModel = await AuthModel.FromAppUser(user, userManager, token);
                        response = new AuthResponseOk(authModel, "Sign in success");
                    }
                }
            }

            return response;
        }

        public async Task<AuthResponse> SignUp(SignUpModel signUpModel, ModelStateDictionary modelState)
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

                if (user != null)
                {
                    var errors = GetErrorsFromParams($"User {signUpModel.UserName} already exists");
                    response = AuthResponseError.GetBadRequestError(errors, errorMessage);
                }
                else
                {
                    response = await TryCreateUser(signUpModel, errorMessage);
                }
            }

            return response;
        }

        public async Task<AuthResponse> SignOut(SignOutModel signOutModel)
        {
            var response = default(AuthResponse);
            var userId = signOutModel.Id;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                var errors = GetErrorsFromParams("User was not found");
                response = AuthResponseError.GetNotFoundError(errors, "Sign out has failed");
            }
            else
            {
                await signInManager.SignOutAsync();
                response = new AuthResponseOk("Sign out success");
            }

            return response;
        }

        private async Task<AuthResponse> TryCreateUser(SignUpModel signUpModel, string errorMessage)
        {
            var response = default(AuthResponse);

            var appUserProfile = new AppUserProfile(signUpModel.FirstName, signUpModel.LastName, signUpModel.MarriageStatus, signUpModel.Gender, signUpModel.DateOfBirth);
            var user = new AppUser(signUpModel.DisplayName, signUpModel.UserName, signUpModel.WorkerId, appUserProfile);
            var result = await userManager.CreateAsync(user, signUpModel.Password);

            appUserProfile.AppUserId = user.Id;
            context.Entry(appUserProfile).State = EntityState.Modified;
            context.SaveChanges();

            if (!result.Succeeded)
            {
                response = AuthResponseError.FromIdentityErrors(result.Errors, errorMessage);
            }
            else
            {
                result = await userManager.AddToRoleAsync(user, AuthRoles.User);
                if (!result.Succeeded)
                {
                    response = AuthResponseError.FromIdentityErrors(result.Errors, errorMessage);
                }
                else
                {
                    response = new AuthResponseOk("Sign up success. Sign in with your credentials");
                }
            }

            return response;
        }

        private IEnumerable<string> GetErrorsFromParams(params string[] @params)
        {
            return @params.AsEnumerable();
        }
    }
}
