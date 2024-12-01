
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [EnumDataType(typeof(TaskStatus))]
        public TaskStatus Status { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
}