using System.ComponentModel.DataAnnotations;

namespace SD_125_BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set;}
        
        public virtual Ticket? Ticket  { get; set; }
        public int? TicketId {get; set;}

        public string? Property {get; set;}

        public string? OldValue { get; set;}

        public string? NewValue {get; set;}

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Changed{ get; set; }

        public string? UserId {get; set;}
        public virtual ApplicationUser? User {get; set;}
    }
}