using Microsoft.AspNetCore.Mvc;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.API.Controllers
{
    [Route("[controller]")]
    public class ApiStartController : Controller
    {
        private readonly IServerService serverService;
        private readonly WorkerCompanyPetContext context;

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
