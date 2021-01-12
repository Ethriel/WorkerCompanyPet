using Broker.TopicExchange;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerCompany.API.Extensions;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.BLL.DTO;
using WorkerCompany.BLL.Responses.ApiResponses;
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
        private readonly TopicExchange topicExchange;

        public WorkersController(ILogger<WorkersController> logger, ICRUDService<Worker, WorkerDTO> workers, TopicExchange topicExchange)
        {
            this.logger = logger;
            this.workers = workers;
            this.topicExchange = topicExchange;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var apiResponse = await workers.ReadAllAsync();
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpGet("get-workers-data")]
        public async Task<IActionResult> GetWorkersIds()
        {
            var allWorkers = await workers.EntitiesAsEnumerableAsync();
            var data = allWorkers.Where(w => !w.Name.Contains(AuthRoles.Admin) && !w.Name.Contains(AuthRoles.Manager))
                                 .Select(w => new { id = w.Id, name = w.Name });
            var response = new ApiOkResponse("Returning workers data", new { workers = data });
            return this.GetActionResult(response, logger);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var apiResponse = await workers.GetByIdAsync(id);
            return this.GetActionResult(apiResponse, logger);
        }

        [Authorize(Roles = AuthRoles.AdminManager)]
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
            using (topicExchange)
            {
                topicExchange.Publish("demo.publish", "demo.exchange", worker);
            }
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
