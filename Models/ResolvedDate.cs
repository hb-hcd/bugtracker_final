namespace SD_125_BugTracker.Models
{
    public class ResolvedDate : CheckResolvedDdl
    {
        public DateTime CalculateResolvedDdl(Ticket ticket)
        {
            return ticket.CalculateDate(ticket.ResponseDate(), 72);
        }
    }
}
