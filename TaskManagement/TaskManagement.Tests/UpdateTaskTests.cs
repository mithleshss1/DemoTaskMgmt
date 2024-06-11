using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagement.Context;
using TaskManagement.Contracts;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.TaskManagement.Tests
{
    public class UpdateTaskTests : IDisposable
    {
        private readonly ITaskServices _taskServices;
        private readonly DbContextOptions<TaskManagementContext> _dbContextOptions;
        private readonly TaskManagementContext _dbContext;

        public UpdateTaskTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseSqlServer("Server=DESKTOP-A6LE2VP\\SSPL_KARTIK;Database=TaskDB;Integrated Security=True;TrustServerCertificate=True")
                .Options;
            _dbContext = new TaskManagementContext(_dbContextOptions);

            _taskServices = new TaskServices(_dbContext);
        }

        [Theory]
        [InlineData("7376F1B0-C935-4FF4-AB7E-08DC89FF408F", "Test Title 1", "Test Description 1", 2, "24-06-2024", "Active")]
        [InlineData("B03ACC38-7D94-41CF-AB7D-08DC89FF408F", "", "Test Description Kartik", 4, "22-06-2024", "Active")]
        [InlineData("6A1AB197-F3B6-4E07-CD3B-08DC89E31CA2", "Test Title 3", "Test Description 3", 2, "28-06-2024", "")]
        public async Task UpdateTask(string taskId, string title, string description, int priority, string dueDate, string status)
        {
            TaskDto taskDto = new()
            {
                Id = new Guid(taskId),
                Title = title,
                Description = description,
                Priority = priority,
                DueDate = Convert.ToDateTime(dueDate),
                Status = status
            };

            var response = await _taskServices.UpdateTaskAsync(taskDto);
            
            Assert.Equal("Success", response);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
