namespace SD_125_BugTracker.Models
{
    public enum RequestType
    {
        FAQ = 1,
        Online = 2,
        SocialMedia = 3    
    }
    public class ServiceRequest : TicketTracking
    {
        public RequestType RequestType { get; set; }

        public ServiceRequest(Ticket ticket, RequestType requestType)
        {
            Ticket = ticket;
            RequestType = requestType;
            CheckResolvedDdl = new ResolvedDate();
        }
        
        
    }
}
