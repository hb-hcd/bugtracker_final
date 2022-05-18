using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL; 

public class TicketAttachmentBll {
    private readonly IRepository<TicketAttachment> _ticketAttachmentRepository;
    public TicketAttachmentBll(IRepository<TicketAttachment> ticketAttachmentRepository) {
        _ticketAttachmentRepository = ticketAttachmentRepository;
    }

    public void CreateAttachment(TicketAttachment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _ticketAttachmentRepository.Add(entity);
    }

    public void UpdateAttachment(TicketAttachment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _ticketAttachmentRepository.Update(entity);
    }

    public void DeleteAttachment(TicketAttachment? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        _ticketAttachmentRepository.Delete(entity);
    }
    
    public void Save() {
        _ticketAttachmentRepository.Save();
    }
}