namespace WorkerCompany.BLL.Responses.ApiResponses
{
    public class ApiOkResponse : ApiResponse
    {
        public object ResponseData { get; set; }
        public ApiOkResponse()
        {

        }
        public ApiOkResponse(string message = null, object data = null)
        {
            SetOkResult(message, data);
        }

        public void SetOkResult(string message = null, object data = null)
        {
            ApiResultStatus = ApiResultStatus.Ok;
            ResponseData = data;
            Message = message;
        }
    }
}
