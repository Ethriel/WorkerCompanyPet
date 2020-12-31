using WorkerCompany.Authentication.Models.Auth;

namespace WorkerCompany.Authentication.Models.Responses
{
    public class AuthResponseOk : AuthResponse
    {
        public AuthModel AuthData { get; set; }
        public AuthResponseOk()
        {
            AuthResponseStatus = AuthResponseStatus.Ok;
        }
        public AuthResponseOk(string message = "Everything is fine")
        {
            AuthResponseStatus = AuthResponseStatus.Ok;
            Message = message;
        }
        public AuthResponseOk(AuthModel authModel, string message = null): base(AuthResponseStatus.Ok, message)
        {
            AuthData = authModel;
        }
    }
}
