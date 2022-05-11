using System.ComponentModel.DataAnnotations;

namespace SD_125_BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id {get; set; }
        public int? TicketId {get; set; }

        public string? FilePath {get; set; }
        public string? Description {get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Created {get; set;}

        public string? UserId {get; set; }
        public virtual ApplicationUser? User {get; set; }
        public string? FileUrl {get; set;}

    }
}
