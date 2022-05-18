using System.ComponentModel.DataAnnotations;

namespace SD_125_BugTracker.Models
{
    public class Ticket
    {
        public int Id {get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Created {get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Updated {get; set;}

        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public int? TicketTypeId { get; set; }
        public virtual TicketType? TicketType { get; set; }
        public int? TicketPriorityId { get; set; }
        public virtual TicketPriority? TicketPriority { get; set; }
        public int? TicketStatusId { get; set; }
        public virtual TicketStatus? TicketStatus { get; set; }
        public string? OwnerUserId { get; set; }
        public virtual ApplicationUser? OwnerUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public virtual ApplicationUser? AssignedToUser { get; set; }


        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        public Ticket()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();

        }
        
        public Ticket Copy() {
            return new() {
                Id = Id,
                Title = Title,
                Created = Created,
                Updated = Updated,
                Description = Description,
                ProjectId = ProjectId,
                TicketTypeId = TicketTypeId,
                TicketPriorityId = TicketPriorityId,
                TicketStatusId = TicketStatusId,
                OwnerUserId = OwnerUserId,  
                AssignedToUserId = AssignedToUserId
            };
        }

    }
}
