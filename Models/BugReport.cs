namespace SD_125_BugTracker.Models
{
    public class BugReport : TicketTracking
    {   
        public Ticket Ticket { get; set; } 
        public string? ErrorCode { get; set; }
        public string? ErrorLogs { get; set; }
        public BugReport(Ticket ticket)
        {
            this.Ticket = ticket;
            
        }
    }
}
