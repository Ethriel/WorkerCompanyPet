using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using WorkerCompany.BLL.DTO;
using WorkerCompany.BLL.Responses.ApiResponses;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.BLL.Services.Implementation;
using WorkerCompany.DAL.Models;
using WorkerCompany.UnitTests.Helpers;
using Xunit;

namespace WorkerCompany.UnitTests.WorkerTests
{
    public class WorkerTest
    {
        [Fact]
        public async Task GetAll()
        {
            var servicesProvider = TestServices.BuildServiceProvider("GetAllWorkersContext");
            var moqContext = new Mock<WorkerCompanyPetContext>();
            var moqService = new GenericEntityService<Worker>(moqContext.Object);
            var first = await moqService.GetByIdAsync(1);
            //var crud = servicesProvider.GetService<ICRUDService<Worker, WorkerDTO>>();
            //var response = await crud.ReadAllAsync();
            //Assert.True(ResponseDataIsNotNull(response));
        }

        private bool ResponseDataIsNotNull(ApiResponse response)
        {
            return (response as ApiOkResponse).ResponseData != null;
        }
    }
}
