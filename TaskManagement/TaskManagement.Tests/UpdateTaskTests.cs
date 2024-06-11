using TaskManagement.Contracts;
using TaskManagement.Models;

namespace TaskManagement.TaskManagement.Tests
{
    public class UpdateTaskTests
    {
        private readonly ITaskServices _taskServices;

        public UpdateTaskTests(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [Theory]
        [InlineData("Test Title 1", "Test Description 1", 2, "24-06-2024", "Active")]
        [InlineData("", "Test Description 2", 4, "22-06-2024", "Active")]
        [InlineData("Test Title 3", "Test Description 3", 2, "28-06-2024", "")]
        public async Task UpdateTask(string title, string description, int priority, string dueDate, string status)
        {
            TaskDto taskDto = new()
            {
                Title = title,
                Description = description,
                Priority = priority,
                DueDate = Convert.ToDateTime(dueDate),
                Status = status
            };

            var response = await _taskServices.UpdateTaskAsync(taskDto);

            Assert.False(response != "Success", $"Task not updated!");
        }
    }
}
