using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagement.Context;
using TaskManagement.Contracts;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Tests
{
    public class CreateTaskTests : IDisposable
    {
        private readonly ITaskServices _taskServices;
        private readonly DbContextOptions<TaskManagementContext> _dbContextOptions;
        private readonly TaskManagementContext _dbContext;

        public CreateTaskTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseSqlServer("Server=DESKTOP-A6LE2VP\\SSPL_KARTIK;Database=TaskDB;Integrated Security=True;TrustServerCertificate=True")
                .Options;
            _dbContext = new TaskManagementContext(_dbContextOptions);

            _taskServices = new TaskServices(_dbContext);
        }

        [Theory]
        [InlineData(null, "Test Description 1", 2, "24-06-2024", "Active")]
        [InlineData("asdasd", "Test Description 2", 4, "22-06-2024", "")]
        [InlineData("Test Title 3", "Test Description 3", 2, "28-06-2024", "Inactive")]
        public async Task CreateTask(string title, string description, int priority, string dueDate, string status)
        {
            TaskDto taskDto = new()
            {
                Title = title,
                Description = description,
                Priority = priority,
                DueDate = Convert.ToDateTime(dueDate),
                Status = status
            };

            var response = await _taskServices.AddTaskAsync(taskDto);

            Assert.Equal("Success", response); // Adjust the assertion based on expected behavior
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
