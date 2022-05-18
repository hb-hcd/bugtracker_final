using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL;

public class TicketNotificationRepository : IRepository<TicketNotification> {
    private readonly ApplicationDbContext _context;
    
    public TicketNotificationRepository(ApplicationDbContext context) {
        _context = context;
    }

    public ICollection<TicketNotification> GetList(Func<TicketNotification, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }
        var ticketNotifications = _context
            .TicketNotifications
            .Include(tn => tn.Ticket)
            .Include(tn => tn.User)
            .Where(whereFunction)
            .ToList();

        return ticketNotifications;
    }

    public TicketNotification? Get(Func<TicketNotification, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }
        var ticketNotification = _context
            .TicketNotifications
            .FirstOrDefault(firstFunction);

        return ticketNotification;
    }

    public void Add(TicketNotification? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketNotifications.Add(entity);
    }

    public void Update(TicketNotification? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketNotifications.Update(entity);
    }

    public void Delete(TicketNotification? entity) {
        switch (entity) {
            case null:
                throw new ArgumentNullException();
            default:
                _context.TicketNotifications.Remove(entity);
                break;
        }
    }

    public void Save() {
        _context.SaveChanges();
    }
}
