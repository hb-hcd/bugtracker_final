using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; } = null!;

        public virtual DbSet<TicketNotification> TicketNotifications { get; set; } = null!;
        public virtual DbSet<TicketComment> TicketComments { get; set; } = null!;
        public virtual DbSet<TicketHistory> TicketHistories { get; set; } = null!;
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; } = null!;
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; } = null!;
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; } = null!;
        public virtual DbSet<TicketType> TicketyTypes { get; set; } = null!;

        public virtual DbSet<AssignedProject> AssignedProjects {get; set; } = null!;


    }
}