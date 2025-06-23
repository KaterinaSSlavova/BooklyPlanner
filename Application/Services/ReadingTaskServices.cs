using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReadingTaskServices: IReadingTaskService
    {
        private readonly IReadingTaskRepository _taskRepo;
        public ReadingTaskServices(IReadingTaskRepository taskRepo)
        {
             _taskRepo = taskRepo;
        }

        public void CreateTask(ReadingTask task)
        {
            ValidateTask(task);
            task.Book.Image.ToLower();
            _taskRepo.CreateTask(task);
        }

        public List<ReadingTask>? LoadUserTasks(int userId)
        {
            return _taskRepo.GetUserTasks(userId);
        }

        public ReadingTask? GetTaskById(int taskId)
        {
            return _taskRepo.GetTaskById(taskId);
        }

        public void MarkTaskAsComplete(ReadingTask task)
        {
            task.IsCompleted = true;    
            task.CompletedAt = DateTime.Now;  
            _taskRepo.UpdateTask(task);
        }

        public void ArchiveTask(ReadingTask task)
        {
            task.IsArchived = true;
            _taskRepo.UpdateTask(task);
        }

        private void ValidateTask(ReadingTask task)
        {
            if (task == null) throw new ArgumentNullException("Invalid data!");
            if (task.DueDate == null || task.DueDate < DateTime.Now) throw new ArgumentException("Invalid due date!");
            if (task.Book.Title == null) throw new ArgumentNullException("Invalid book title!");
            if (task.Book.Author == null) throw new ArgumentNullException("Invalid book author!");
        }
    }
}
