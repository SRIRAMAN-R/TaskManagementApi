using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = TaskManagementApi.Models.Task; // Alias to avoid ambiguity

namespace TaskManagementApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetTasks(string? status, DateTime? dueDate, int pageNumber, int pageSize);
        Task<TaskEntity?> GetTaskById(int id);
        Task<TaskEntity> CreateTask(TaskEntity task);
        Task<bool> UpdateTask(TaskEntity task);
        Task<bool> DeleteTask(int id);
        Task<IEnumerable<TaskEntity>> GetAllTasks();
    }
}
