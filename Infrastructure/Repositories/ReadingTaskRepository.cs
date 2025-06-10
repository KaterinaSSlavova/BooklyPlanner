using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .Include(t => t.Book)
                .Where(t => t.UserId ==  userId && t.IsArchived == false)
                .ToList();
        }

        public ReadingTask? GetTaskById(int taskId)
        {
            return _context.ReadingTasks
                .Include(t => t.Book)
                .FirstOrDefault(t => t.Id == taskId);
        }

        public void UpdateTask(ReadingTask task)
        {
            _context.ReadingTasks.Update(task);
            _context.SaveChanges();
        }
    }
}
