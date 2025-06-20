using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApiTaskClient
    {
        Task<List<ReadingTask>?> LoadUserTasks(int userId);
        Task<ReadingTask?> GetTaskById(int taskId);
        Task CreateTask(ReadingTask task);
        Task MarkAsComplete(ReadingTask task);
        Task ArchiveTask(ReadingTask task);
    }
}
