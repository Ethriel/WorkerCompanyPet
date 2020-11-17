using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WorkerCompany.BLL.Services.Abstraction;

namespace WorkerCompany.BLL.Services.Implementation
{
    public class ServerService : IServerService
    {
        private readonly IWebHostEnvironment webHost;
        private readonly HttpContext httpContext;
        public ServerService(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            this.webHost = webHost;
            httpContext = httpContextAccessor.HttpContext;
        }
        public string GetSwaggerHref()
        {
            var authority = GetAuthority();
            var url = GetURL(authority);
            var swagger = string.Concat(url, "swagger/index.html");

            return swagger;

        }
        private string GetAuthority()
        {
            return httpContext.Request.Host.Value;
        }

        private string GetURL(string authority)
        {
            return string.Concat("https://", authority, "/");
        }
    }
}
