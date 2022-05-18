namespace SD_125_BugTracker.Models
{
    public class TicketPriority
    {
        public int Id { get; set; }
        public Priority Name { get; set; }
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
    }
}