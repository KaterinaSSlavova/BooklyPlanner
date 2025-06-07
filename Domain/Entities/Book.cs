namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? Author { get; set; } 
        public string? Image { get; set; }
        public int Pages { get; set; }

        public ICollection<ReadingTask> ReadingTasks { get; set; } = new List<ReadingTask>();
    }
}
