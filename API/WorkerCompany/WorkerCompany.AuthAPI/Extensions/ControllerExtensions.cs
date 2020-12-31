using Microsoft.AspNetCore.Mvc;
using WorkerCompany.Authentication.Models.Responses;

namespace WorkerCompany.AuthAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult GetResponseResult(this Controller controller, AuthResponse authResponse)
        {
            switch (authResponse.AuthResponseStatus)
            {
                case AuthResponseStatus.Ok:
                    return controller.Ok(GetAuthData(authResponse));
                case AuthResponseStatus.NotFound:
                    return controller.NotFound(GetAuthData(authResponse));
                case AuthResponseStatus.BadRequest:
                default:
                    return controller.BadRequest(GetAuthData(authResponse));
            }
        }

        private static object GetAuthData(AuthResponse authResponse)
        {
            if (authResponse is AuthResponseOk)
            {
                return GetOkAuthData(authResponse);
            }
            else
            {
                return GetErrorAuthData(authResponse);
            }
        }

        private static object GetErrorAuthData(AuthResponse authResponse)
        {
            var errors = ((AuthResponseError)authResponse).Errors;
            return new { errors = errors, message = authResponse.Message };
        }

        private static object GetOkAuthData(AuthResponse authResponse)
        {
            var authData = ((AuthResponseOk)authResponse).AuthData;
            return new { authData = authData, message = authResponse.Message };
        }
    }
}
