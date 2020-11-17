using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorkerCompany.BLL.Responses.ApiResponses
{
    public class ApiErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        public string LoggerMessage { get; set; }

        public ApiErrorResponse()
        {

        }

        public ApiErrorResponse(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            SetErrorResult(apiResultStatus, loggerMessage, message, errors);
        }

        public void SetErrorResult(ApiResultStatus apiResultStatus, string loggerMessage, string message = null, IEnumerable<string> errors = null)
        {
            ApiResultStatus = apiResultStatus;
            LoggerMessage = loggerMessage;
            Message = message;
            Errors = errors;
        }
    }
}
