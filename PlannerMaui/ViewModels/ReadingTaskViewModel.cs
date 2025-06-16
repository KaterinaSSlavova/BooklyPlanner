using Domain.Entities;

namespace Planner.ViewModels
{
    public class ReadingTaskViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        public Book? Book { get; set; }
    }
}
