namespace SD_125_BugTracker.Models
{
    public class TicketStatus
    {
        public int Id{get; set; }
        public Status? Status { get; set; }

    }

    public enum Status
    {
        Unassigned, 
        Assigned, 
        InProgress, 
        Completed
    }
}
