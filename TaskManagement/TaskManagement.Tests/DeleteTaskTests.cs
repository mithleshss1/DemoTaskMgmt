using TaskManagement.Contracts;
using TaskManagement.Models;

namespace TaskManagement.TaskManagement.Tests
{
    public class DeleteTaskTests
    {
        private readonly ITaskServices _taskServices;

        public DeleteTaskTests(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [Theory]
        [InlineData("6D521557-76C5-46CC-50B3-08DC89633A17")]
        [InlineData("")]
        [InlineData("16B69D11-7B1B-7G49-DF8E-08DC897BDB81")]
        public async Task DeleteTask(string taskId)
        {
            var taskGuidId = new Guid(taskId);
            var response = await _taskServices.RemoveTaskByIdAsync(taskGuidId);

            Assert.False(response != "Success", $"Task not updated!");
        }
    }
}
