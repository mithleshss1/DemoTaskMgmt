using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.Context;
using TaskManagement.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class TaskServices(TaskManagementContext context) : ITaskServices
    {
        private readonly TaskManagementContext _context = context;

        public async Task<List<TaskDto>?> GetFilteredTasks(FilterDto filters)
        {
            try
            {
                var taskDtos = new List<TaskDto>();

                var taskList = await _context.Tasks.ToListAsync();
                if (taskList == null || taskList.Count == 0)
                    return taskDtos;

                if (filters.Priority > 0)
                    taskList = taskList.Where(x => x.Priority == filters.Priority).ToList();
                if (!string.IsNullOrEmpty(filters.Status))
                    taskList = taskList.Where(x => x.Status == filters.Status).ToList();

                taskList.ForEach(task =>
                {
                    var dto = new TaskDto
                    {
                        Id = task.TaskId,
                        Title = task.Title,
                        Description = task.Description,
                        Priority = task.Priority,
                        Status = task.Status,
                        DueDate = Convert.ToDateTime(task.DueDate.ToString("yyyy-MM-dd")),
                    };
                    taskDtos.Add(dto);
                });
                return taskDtos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> AddTaskAsync(TaskDto taskDto)
        {
            try
            {
                var newTask = new Tasks
                {
                    TaskId = new Guid(),
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Priority = taskDto.Priority,
                    DueDate = taskDto.DueDate,
                    Status = taskDto.Status
                };
                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Tasks?> GetTaskByIdAsync(Guid taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                return task;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> UpdateTaskAsync(TaskDto task)
        {
            try
            {
                var taskToUpdate = new Tasks
                {
                    TaskId = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    Status = task.Status,
                    DueDate = task.DueDate
                };
                _context.Tasks.Update(taskToUpdate);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> RemoveTaskByIdAsync(Guid taskId)
        {
            try
            {
                var taskToRemove = _context.Tasks.Find(taskId);
                if (taskToRemove == null)
                    return "Task not found!";

                _context.Tasks.Remove(taskToRemove);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
