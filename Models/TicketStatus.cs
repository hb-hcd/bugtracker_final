namespace SD_125_BugTracker.Models
{
    public class TicketStatus
    {
        public int Id{get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Open = 1, 
        Assigned = 2, 
        InProgress = 3, 
        Resolved = 4
    }
}