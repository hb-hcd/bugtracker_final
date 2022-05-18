using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL;

public class TicketHistoryRepository : IRepository<TicketHistory> {
    private readonly ApplicationDbContext _context;
    
    public TicketHistoryRepository(ApplicationDbContext context) {
        _context = context;
    }

    public ICollection<TicketHistory> GetList(Func<TicketHistory, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }

        var ticketHistories = _context
            .TicketHistories
            .Include(th => th.User)
            .Include(th => th.Ticket)
            .Where(whereFunction).ToList();

        return ticketHistories;
    }

    public TicketHistory? Get(Func<TicketHistory, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }

        var ticketHistory = _context
            .TicketHistories
            .Include(th => th.User)
            .Include(th => th.Ticket)
            .FirstOrDefault(firstFunction);

        return ticketHistory;
    }

    public void Add(TicketHistory? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketHistories.Add(entity);
    }

    public void Update(TicketHistory? entity) {
        throw new NotImplementedException();
    }

    public void Delete(TicketHistory? entity) {
        throw new NotImplementedException();
    }

    public void Save() {
        _context.SaveChanges();
    }
}