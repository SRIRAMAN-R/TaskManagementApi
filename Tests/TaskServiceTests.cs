using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using TaskManagementApi.Models;
using TaskManagementApi.Services;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Tests
{
    [TestFixture]
    public class TaskServiceTests
    {
        private TaskDbContext _context;
        private TaskService _taskService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            _context = new TaskDbContext(options);
            _taskService = new TaskService(_context, Mock.Of<Microsoft.Extensions.Logging.ILogger<TaskService>>());
        }

        [Test]
        public async System.Threading.Tasks.Task CreateTask_AddsTaskToDatabase()
        {
            var task = new TaskManagementApi.Models.Task { Title = "Test Task", DueDate = System.DateTime.UtcNow };
            var result = await _taskService.CreateTask(task);

            Assert.IsNotNull(result);
            Assert.AreEqual(task.Title, result.Title);
        }
    }
}
