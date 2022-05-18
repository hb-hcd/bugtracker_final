using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL;

public class TicketAttachmentRepository : IRepository<TicketAttachment> {
    private readonly ApplicationDbContext _context;
    
    public TicketAttachmentRepository(ApplicationDbContext context) {
        _context = context;
    }

    public ICollection<TicketAttachment> GetList(Func<TicketAttachment, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }

        var ticketAttachments = _context.TicketAttachments.Include(ta => ta.User).Where(whereFunction).ToList();

        return ticketAttachments;
    }

    public TicketAttachment? Get(Func<TicketAttachment, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }

        var ticketAttachment = _context
            .TicketAttachments
            .Include(ta => ta.User)
            .FirstOrDefault(firstFunction);

        return ticketAttachment;
    }

    public void Add(TicketAttachment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketAttachments.Add(entity);
    }

    public void Update(TicketAttachment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketAttachments.Update(entity);
    }

    public void Delete(TicketAttachment? entity) {
        switch (entity) {
            case null:
                throw new ArgumentNullException();
            default:
                _context.TicketAttachments.Remove(entity);
                break;
        }
    }

    public void Save() {
        _context.SaveChanges();
    }
}