using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkerCompany.BLL.Responses.ApiResponses;

namespace WorkerCompany.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult GetActionResult(this Controller controller, ApiResponse apiResponse, ILogger logger)
        {
            switch (apiResponse.ApiResultStatus)
            {
                case ApiResultStatus.Ok:
                    return controller.Ok(apiResponse);
                case ApiResultStatus.NotFound:
                    logger.LogWarning((apiResponse as ApiErrorResponse).LoggerMessage);
                    return controller.NotFound(apiResponse);
                case ApiResultStatus.BadRequest:
                default:
                    logger.LogWarning((apiResponse as ApiErrorResponse).LoggerMessage);
                    return controller.BadRequest(apiResponse);
            }
        }
    }
}
