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
            var random = new Random();
            var number = random.Next(1, 20);
            var newName = $"{number}. Worker was renamed by test";
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
            var service = GetService<Worker>();

            var workerToAdd = new Worker
            {
                Name = "Worker for unit test",
                CompanyId = 1
            };

            service.Add(workerToAdd);
            var commited = await service.CommitChangesAsync();

            service.Delete(workerToAdd);
            commited = await service.CommitChangesAsync();

            var deletedWorker = await service.GetEntityByConditionAsync(x => x.Name.Equals(workerToAdd.Name));
            Assert.Null(deletedWorker);
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
