using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? Author { get; set; } 
        public string? Image { get; set; }
        public int Pages { get; set; }

        [JsonIgnore]
        public ICollection<ReadingTask> ReadingTasks { get; set; } = new List<ReadingTask>();

        public Book(string title, string author, string image, int pages)
        {
            this.Title= title;
            this.Author= author;
            this.Image= image;
            this.Pages= pages;
        }

        public Book()
        {
            
        }
    }
}
