using TaskManagerAPI.Services;
using TaskManagerAPI.Models;
using Xunit;

namespace TaskManagerTests
{
    public class TaskTests
    {
        [Fact]
        public void AddTask_ShouldIncreaseCount()
        {
            var service = new TaskService();

            service.Add(new TaskItem { Name = "Test Task" });

            Assert.Single(service.GetAll());
        }
    }
}