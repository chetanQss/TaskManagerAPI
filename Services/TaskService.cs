using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskService
    {
        private static List<TaskItem> tasks = new List<TaskItem>();

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem Add(TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return task;
        }

        public void Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                tasks.Remove(task);
        }
    }
}