namespace SD_125_BugTracker.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public Type? Name { get; set; }


    }
    public enum Type
    {
        Incident, 
        ServiceRequest, 
        InformationRequest
    }
}
