using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL; 

public class TicketCommentBll {
    private readonly IRepository<TicketComment> _ticketCommentRepository;
    public TicketCommentBll(IRepository<TicketComment> ticketCommentRepository) {
        _ticketCommentRepository = ticketCommentRepository;
    }

    public void CreateComment(TicketComment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _ticketCommentRepository.Add(entity);
    }

    public void UpdateComment(TicketComment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _ticketCommentRepository.Update(entity);
    }

    public void DeleteComment(TicketComment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        _ticketCommentRepository.Delete(entity);
    }
    
    public void Save() {
        _ticketCommentRepository.Save();
    }

    public List<TicketComment> GetTicketCommentsByTicketId(int? id)
    {
        return _ticketCommentRepository.GetList(c => c.TicketId == id).ToList();
    }

    public TicketComment GetCommentById(int id)
    {
       return _ticketCommentRepository.Get(c=>c.Id == id);
    }
}