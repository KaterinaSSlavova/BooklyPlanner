using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReadingTaskService
    {
        void CreateTask(ReadingTask task);
        List<ReadingTask>? LoadUserTasks(int userId);
        ReadingTask? GetTaskById(int taskId);
        void MarkTaskAsComplete(ReadingTask task);
        void ArchiveTask(ReadingTask task);
    }
}
