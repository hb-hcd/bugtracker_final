using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL;

public class TicketCommentRepository : IRepository<TicketComment> {
    private readonly ApplicationDbContext _context;
    
    public TicketCommentRepository(ApplicationDbContext context) {
        _context = context;
    }

    public ICollection<TicketComment> GetList(Func<TicketComment, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }
        
        var ticketComments = _context.TicketComments.Include(tc => tc.User).Where(whereFunction).ToList();

        return ticketComments;
    }

    public TicketComment? Get(Func<TicketComment, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }
        
        var ticketComment = _context
            .TicketComments
            .Include(tc => tc.User)
            .FirstOrDefault(firstFunction);

        return ticketComment;
    }

    public void Add(TicketComment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }

        _context.TicketComments.Add(entity);

    }

    public void Update(TicketComment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        _context.TicketComments.Update(entity);
    }

    public void Delete(TicketComment? entity) {
        switch (entity) {
            case null:
                throw new ArgumentNullException();
            default:
                _context.TicketComments.Remove(entity);
                break;
        }
    }

    public void Save() {
        _context.SaveChanges();
    }
}