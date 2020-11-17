using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorkerCompany.BLL.Responses.ApiResponses
{
    public enum ApiResultStatus
    {
        Ok,
        NotFound,
        BadRequest
    }

    public class ApiResponse
    {
        [JsonIgnore]
        public ApiResultStatus ApiResultStatus { get; set; }
        public string Message { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(ApiResultStatus apiResultStatus, string message = null)
        {
            SetResult(apiResultStatus, message);
        }

        public void SetResult(ApiResultStatus apiResultStatus, string message = null)
        {
            ApiResultStatus = apiResultStatus;
            Message = message;
        }

        public static ApiOkResponse GetOkResponse(string message = null, object data = null)
        {
            return new ApiOkResponse(message, data);
        }

        public static ApiErrorResponse GetErrorResponse(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            return new ApiErrorResponse(apiResultStatus, loggerMessage, message, errors);
        }
    }
}
