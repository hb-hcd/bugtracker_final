namespace SD_125_BugTracker.Models
{
    public class TicketPriority
    {
        public int Id{get; set; }
        public Priority? Name{get; set; }
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }
}
