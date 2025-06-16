namespace Planner.ViewModels
{
    public class BooklyToPlanner
    {
        public string Username { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Image { get; set; }
        public int Pages { get; set; }
        public DateTime DueDate { get; set; }
    }
}
