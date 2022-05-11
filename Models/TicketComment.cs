using System.ComponentModel.DataAnnotations;

namespace SD_125_BugTracker.Models
{
    public class TicketComment
    {
        public int Id {get; set;}
        public string? Comment{get; set;}

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Created{get; set;}

        public int? TicketId {get; set;}
        public string? UserId{get; set;}
        public virtual ApplicationUser? User {get; set;}
    
    }
}
