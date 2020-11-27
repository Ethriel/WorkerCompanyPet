using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.DAL.Models;
using WorkerCompany.UnitTests.Helpers;
using Xunit;

namespace WorkerCompany.UnitTests.WorkerTests
{
    public class WorkerTest
    {
        private readonly IServiceProvider serviceProvider;
        public WorkerTest()
        {
            serviceProvider = TestServices.BuildServiceProvider();
        }
        [Fact]
        public async Task GetAll()
        {
            var service = GetService<Worker>();
            var data = await service.ReadAll().ToArrayAsync();
            Assert.NotEmpty(data);
        }

        [Fact]
        public async Task GetById()
        {
            var id = 1;

            var context = GetContext();
            var first = await context.Worker.FirstOrDefaultAsync();

            var service = GetService<Worker>();
            var second = await service.GetByIdAsync(id);
            Assert.Equal(first.Name, second.Name);
        }

        [Fact]
        public async Task Update()
        {
            var newName = "Worker 1 renamed by test";
            var id = 1;

            var service = GetService<Worker>();
            var before = await service.GetByIdAsync(id);
            before.Name = newName;

            var commited = await service.CommitChangesAsync();

            var after = await service.GetByIdAsync(id);
            Assert.Equal(after.Name, newName);
        }

        [Fact]
        public async Task Delete()
        {
            var context = GetContext();
            var service = GetService<Worker>();

            var worker = await context.Worker.OrderBy(x => x.Id).LastOrDefaultAsync();
            service.Delete(worker);

            var commited = await service.CommitChangesAsync();

            var all = await service.ReadAll().ToArrayAsync();
            Assert.DoesNotContain<Worker>(worker, all);
        }

        private IGenericEntityService<T> GetService<T>() where T : class
        {
            return serviceProvider.GetService<IGenericEntityService<T>>();
        }

        private WorkerCompanyPetContext GetContext()
        {
            return serviceProvider.GetService<WorkerCompanyPetContext>();
        }

    }
}
