using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services;
using Task = TaskManagementApi.Models.Task;  // Alias the model task here
using TaskStatus = TaskManagementApi.Models.TaskStatus; // Alias TaskStatus
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using CsvHelper;
using System.IO;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] string? status, [FromQuery] DateTime? dueDate, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _taskService.GetTasks(status, dueDate, pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null) return NotFound(new { Message = $"Task with id {id} not found" });
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Task task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdTask = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Task task)
        {
            if (id != task.Id) return BadRequest("ID mismatch.");
            if (!await _taskService.UpdateTask(task)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (!await _taskService.DeleteTask(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("export")]
        public IActionResult ExportTasksToCsv()
        {
            var tasks = _taskService.GetTasks(null, null, 1, int.MaxValue).Result;

            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(tasks);
                streamWriter.Flush(); // Ensure all data is flushed to the memory stream
            }

            var fileContent = memoryStream.ToArray(); // Convert the stream to a byte array
            return File(fileContent, "text/csv", "tasks.csv"); // Return file content directly
        }
    }
}
