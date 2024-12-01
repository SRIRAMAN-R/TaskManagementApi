using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskManagementApi.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task GetTasks_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/tasks");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
