using Microsoft.AspNetCore.Mvc;
using WorkerCompany.BLL.Services.Abstraction;

namespace WorkerCompany.API.Controllers
{
    [Route("[controller]")]
    public class ApiStartController : Controller
    {
        private readonly IServerService serverService;

        public ApiStartController(IServerService serverService)
        {
            this.serverService = serverService;
        }
        [HttpGet]
        public IActionResult Start()
        {
            ViewBag.Text = "Server is running";
            ViewBag.Href = serverService.GetSwaggerHref();
            return View();
        }
    }
}
