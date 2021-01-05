using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WorkerCompany.API.Extensions;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.BLL.DTO;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : Controller
    {
        private readonly ILogger<WorkersController> logger;
        private readonly ICRUDService<Worker, WorkerDTO> workers;

        public WorkersController(ILogger<WorkersController> logger, ICRUDService<Worker, WorkerDTO> workers)
        {
            this.logger = logger;
            this.workers = workers;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var apiResponse = await workers.ReadAllAsync();
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var apiResponse = await workers.GetByIdAsync(id);
            return this.GetActionResult(apiResponse, logger);
        }

        [Authorize(Roles = AuthRoles.Admin)]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var apiResponse = await workers.DeleteAsync(id);
            return this.GetActionResult(apiResponse, logger);
        }

        [Authorize(Roles = AuthRoles.AdminManager)]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] WorkerDTO worker)
        {
            var apiResponse = await workers.UpdateAsync(worker.Id, worker);
            return this.GetActionResult(apiResponse, logger);
        }

        [Authorize(Roles = AuthRoles.AdminManager)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] WorkerDTO worker)
        {
            var apiResponse = await workers.AddAsync(worker);
            return this.GetActionResult(apiResponse, logger);
        }
    }
}
