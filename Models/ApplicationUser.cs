using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD_125_BugTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Project> Projects { get; set; }

        [InverseProperty("OwnerUser")]
        public virtual ICollection<Ticket> OwnedTickets { get; set; }

        [InverseProperty("AssignedToUser")]
        public virtual ICollection<Ticket> AssignedTickets { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotification { get; set; }
       
       
    }
}
