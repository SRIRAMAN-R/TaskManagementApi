using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Controllers;
using TaskManagementApi.Services;

namespace TaskManagementApi.Tests
{
    [TestFixture]
    public class TasksControllerTests
    {
        private Mock<ITaskService> _mockTaskService;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _mockTaskService = new Mock<ITaskService>();
            _controller = new TasksController(_mockTaskService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task GetTasks_ReturnsOkResult()
        {
            _mockTaskService.Setup(service => service.GetTasks(null, null, 1, 10))
                            .ReturnsAsync(new List<TaskManagementApi.Models.Task> { new TaskManagementApi.Models.Task { Id = 1, Title = "Test Task" } });

            var result = await _controller.GetTasks(null, null);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
