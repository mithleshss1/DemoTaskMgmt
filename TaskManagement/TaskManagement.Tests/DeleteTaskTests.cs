using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagement.Context;
using TaskManagement.Contracts;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.TaskManagement.Tests
{
    public class DeleteTaskTests : IDisposable
    {
        private readonly ITaskServices _taskServices;
        private readonly DbContextOptions<TaskManagementContext> _dbContextOptions;
        private readonly TaskManagementContext _dbContext;

        public DeleteTaskTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseSqlServer("Server=DESKTOP-A6LE2VP\\SSPL_KARTIK;Database=TaskDB;Integrated Security=True;TrustServerCertificate=True")
                .Options;
            _dbContext = new TaskManagementContext(_dbContextOptions);

            _taskServices = new TaskServices(_dbContext);
        }

        [Theory]
        [InlineData("82CFED60-C592-49F1-CF1E-08DC8972F245")]
        [InlineData("7A2E6278-2295-4D58-B8D3-08DC89F9E7D3")]
        [InlineData("6A1AB197-F3B6-4E07-CD3B-08DC89E31CA2")]
        public async Task DeleteTask(string taskId)
        {
            var taskGuidId = new Guid(taskId);

            var response = await _taskServices.RemoveTaskByIdAsync(taskGuidId);           

            Assert.Equal("Success", response);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
