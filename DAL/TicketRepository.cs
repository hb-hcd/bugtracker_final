using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL;

public class TicketRepository : IRepository<Ticket> {
    private readonly ApplicationDbContext _context;
    
    public TicketRepository(ApplicationDbContext context) {
        _context = context;
    }

    public ICollection<Ticket> GetList(Func<Ticket, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }

        var tickets = _context
            .Tickets
            .Include(t => t.Project)
            .Include(t => t.OwnerUser)
            .Include(t => t.AssignedToUser)
            .Include(t => t.TicketType)
            .Include(t => t.TicketPriority)
            .Include(t => t.TicketStatus)
            .Where(whereFunction)
            .ToList();

        return tickets;
    }

    public Ticket? Get(Func<Ticket, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }

        var ticket = _context
            .Tickets
            .Include(t => t.Project)
            .Include(t => t.OwnerUser)
            .Include(t => t.AssignedToUser)
            .Include(t => t.TicketType)
            .Include(t => t.TicketPriority)
            .Include(t => t.TicketStatus)
            .AsNoTracking()
            .FirstOrDefault(firstFunction);

        return ticket;
    }

    public void Add(Ticket? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.Tickets.Add(entity);
    }

    public void Update(Ticket? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.Tickets.Update(entity);
    }

    public void Delete(Ticket? entity) {
        throw new NotImplementedException();
    }

    public void Save() {
        _context.SaveChanges();
    }
}