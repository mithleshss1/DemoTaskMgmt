using TaskManagement.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Tests
{
    public class CreateTaskTests
    {
        private readonly ITaskServices _taskServices;

        public CreateTaskTests(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [Theory]
        [InlineData("", "Test Description 1", 2, "24-06-2024", "Active")]
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

            Assert.False(response != "Success", $"Task not created!");
        }
    }
}
