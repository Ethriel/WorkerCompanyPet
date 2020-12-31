using Newtonsoft.Json;

namespace WorkerCompany.Authentication.Models.Responses
{
    public enum AuthResponseStatus
    {
        Ok,
        NotFound,
        BadRequest
    }

    public class AuthResponse
    {
        [JsonIgnore]
        public AuthResponseStatus AuthResponseStatus { get; set; }
        public string Message { get; set; }
        public AuthResponse()
        {

        }
        public AuthResponse(AuthResponseStatus authResponseStatus, string message = null)
        {
            AuthResponseStatus = authResponseStatus;
            Message = message;
        }
    }
}
