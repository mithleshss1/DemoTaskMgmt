using TaskManagement.Models;

namespace TaskManagement.Contracts
{
    public interface ITaskServices
    {
        Task<List<TaskDto>?> GetFilteredTasks(FilterDto filters);

        Task<string> AddTaskAsync(TaskDto taskDto);

        Task<Tasks?> GetTaskByIdAsync(Guid taskId);

        Task<string> UpdateTaskAsync(TaskDto taskDto);

        Task<string> RemoveTaskByIdAsync(Guid taskId);
    }
}
