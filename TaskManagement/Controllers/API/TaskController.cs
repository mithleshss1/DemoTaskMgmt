using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Context;
using TaskManagement.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;

        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [HttpPost("filtered-tasks-list")]
        public async Task<IActionResult> GetFilteredTasks(FilterDto filters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taskServices.GetFilteredTasks(filters);

            return response?.Count > 0 ? StatusCode(StatusCodes.Status200OK, response) : StatusCode(StatusCodes.Status204NoContent, "No tasks found!");
        }

        [HttpPost("add-task")]
        public async Task<IActionResult> CreateTask(TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taskServices.AddTaskAsync(taskDto);

            return response == "Success" ? StatusCode(StatusCodes.Status201Created) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet("get-task")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            if (taskId == Guid.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, "Not a valid Id");

            var response = await _taskServices.GetTaskByIdAsync(taskId);

            return response != null ? StatusCode(StatusCodes.Status200OK, response) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut("modify-task")]
        public async Task<IActionResult> UpdateTask(TaskDto task)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);

            var response = await _taskServices.UpdateTaskAsync(task);

            return response == "Success" ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpDelete("remove-task")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            if (taskId == Guid.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, "Not a valid Id");

            var response = await _taskServices.RemoveTaskByIdAsync(taskId);

            return response == "Success" ? StatusCode(StatusCodes.Status200OK) : response == "Task not found!" ? StatusCode(StatusCodes.Status404NotFound, response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
