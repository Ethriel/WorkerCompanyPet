using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace WorkerCompany.Authentication.Models.Responses
{
    public class AuthResponseError : AuthResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public AuthResponseError()
        {

        }
        public AuthResponseError(AuthResponseStatus authResponseStatus, IEnumerable<string> errors = null, string message = null) : base(authResponseStatus, message)
        {
            Errors = errors;
        }

        public static AuthResponse GetNotFoundError(IEnumerable<string> errors = null, string message = null)
        {
            return new AuthResponseError(AuthResponseStatus.NotFound, errors, message);
        }

        public static AuthResponse GetBadRequestError(IEnumerable<string> errors = null, string message = null)
        {
            return new AuthResponseError(AuthResponseStatus.BadRequest, errors, message);
        }

        public static AuthResponse FromModelStateErrors(IReadOnlyDictionary<string, ModelStateEntry> modelState, string message = null)
        {
            var errors = modelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors)
                                          .Select(error => error.ErrorMessage);

            return new AuthResponseError(AuthResponseStatus.BadRequest, errors, message);
        }

        public static AuthResponse FromIdentityErrors(IEnumerable<IdentityError> identityErrors, string message = null)
        {
            var errors = identityErrors.Select(error => $"{error.Code}: {error.Description}");
            return new AuthResponseError(AuthResponseStatus.BadRequest, errors, message);
        }
    }
}
