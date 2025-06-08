using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadingTaskRepository
    {
        void CreateTask(ReadingTask task);
        List<ReadingTask> GetUserTasks(int userId);
        ReadingTask? GetTaskById(int taskId);
        void UpdateTask(ReadingTask task);
    }
}
