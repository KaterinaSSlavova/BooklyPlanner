using System.Runtime;

namespace Domain.Entities
{
    public class ReadingTask
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsArchived { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
