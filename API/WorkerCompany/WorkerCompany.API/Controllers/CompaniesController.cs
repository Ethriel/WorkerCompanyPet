using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WorkerCompany.API.Extensions;
using WorkerCompany.BLL.DTO;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ILogger<CompaniesController> logger;
        private readonly ICRUDService<Company, CompanyDTO> companies;

        public CompaniesController(ILogger<CompaniesController> logger, ICRUDService<Company, CompanyDTO> companies)
        {
            this.logger = logger;
            this.companies = companies;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var apiResponse = await companies.ReadAllAsync();
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var apiResponse = await companies.GetByIdAsync(id);
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var apiResponse = await companies.DeleteAsync(id);
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyDTO company)
        {
            var apiResponse = await companies.UpdateAsync(company.Id, company);
            return this.GetActionResult(apiResponse, logger);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CompanyDTO company)
        {
            var apiResponse = await companies.AddAsync(company);
            return this.GetActionResult(apiResponse, logger);
        }
    }
}
