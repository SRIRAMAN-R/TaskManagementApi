using Microsoft.EntityFrameworkCore;
using Task = TaskManagementApi.Models.Task;  // Alias Task model here
using TaskManagementApi.Models;

namespace TaskManagementApi
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
    }
}
