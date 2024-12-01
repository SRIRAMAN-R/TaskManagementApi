using System.Linq;
using Microsoft.EntityFrameworkCore;
using Task = TaskManagementApi.Models.Task;  // Alias for TaskManagementApi.Models.Task
using System.Threading.Tasks;  // For async Task
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TaskEntity = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(TaskDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Task>> GetTasks(string? status, DateTime? dueDate, int pageNumber, int pageSize)
        {
            var query = _context.Tasks.AsQueryable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status.ToString() == status);

            if (dueDate.HasValue)
                query = query.Where(t => t.DueDate < dueDate);

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Task?> GetTaskById(int id) => await _context.Tasks.FindAsync(id);

        public async Task<Task> CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Task created with ID {task.Id}");  // Log task creation
            return task;
        }

        public async Task<bool> UpdateTask(Task task)
        {
            // Log before updating the task
            _logger.LogInformation($"Updating task with ID {task.Id}");

            _context.Entry(task).State = EntityState.Modified;
            bool result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                _logger.LogInformation($"Task with ID {task.Id} updated successfully.");
            }
            else
            {
                _logger.LogWarning($"Failed to update task with ID {task.Id}. No changes made.");
            }

            return result;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning($"Attempt to delete non-existent task with ID {id}");
                return false;
            }

            _context.Tasks.Remove(task);
            bool result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                _logger.LogInformation($"Task with ID {id} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"Failed to delete task with ID {id}. No changes made.");
            }

            return result;
        }

        public async Task<IEnumerable<Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}
