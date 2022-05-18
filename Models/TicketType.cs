namespace SD_125_BugTracker.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public Type Name { get; set; }


    }
    public enum Type
    {
        Incident = 1, 
        ServiceRequest = 2, 
        InformationRequest = 3 
    }
}