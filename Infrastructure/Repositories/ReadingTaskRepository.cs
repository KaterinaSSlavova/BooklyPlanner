using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ReadingTaskRepository: IReadingTaskRepository
    {
        private readonly DBContext _context;
        public ReadingTaskRepository(DBContext context)
        {
            _context = context;
        }

        public void CreateTask(ReadingTask task)
        {
            _context.ReadingTasks.Add(task);
            _context.SaveChanges();
        }

        public List<ReadingTask> GetUserTasks(int userId)
        {
            return _context.ReadingTasks
                .Where(t => t.UserId ==  userId && t.IsArchived == false)
                .ToList();
        }

        public ReadingTask? GetTaskById(int taskId)
        {
            return _context.ReadingTasks.Find(taskId);
        }

        public void UpdateTask(ReadingTask task)
        {
            _context.ReadingTasks.Update(task);
            _context.SaveChanges();
        }
    }
}
