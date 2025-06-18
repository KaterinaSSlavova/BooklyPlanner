using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApiTaskClient
    {
        Task<List<ReadingTask>?> LoadUserTasks(int userId);
        Task<ReadingTask?> GetTaskById(int taskId);
        Task<bool> CreateTask(ReadingTask task);
        Task<bool> MarkAsComplete(ReadingTask task);
        Task<bool> ArchiveTask(ReadingTask task);
    }
}
