namespace SD_125_BugTracker.Models
{
    public class Project
    {
        public int Id {get; set; }
        public string Name { get; set; }

        public bool IsArchived { get; set; } = false;

        public virtual ApplicationUser? User { get; set; }
     
        public virtual ICollection<Ticket> Tickets { get;set;}

        public Project()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}