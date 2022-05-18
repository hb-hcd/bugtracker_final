using System.Data;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL;

public class TicketBusinessLogic {
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<TicketHistory> _ticketHistoryRepository;
    private readonly IRepository<ProjectUser> _projectUserRepository;
    
    public TicketBusinessLogic(IRepository<Ticket> ticketRepository, IRepository<TicketHistory> ticketHistoryRepository, IRepository<ProjectUser> projectUserRepository) {
        _ticketRepository = ticketRepository;
        _ticketHistoryRepository = ticketHistoryRepository;
        _projectUserRepository = projectUserRepository;
    }

    public ICollection<Ticket> GetAllTickets() {
        var tickets = _ticketRepository.GetList(_ => true);
        return tickets;
    }

    public ICollection<Ticket> GetOwnedTickets(string? userId) {
        if (userId is null) {
            throw new ArgumentNullException();
        }

        var tickets = _ticketRepository.GetList(t => t.OwnerUserId == userId);
        return tickets;
    }
    
    public ICollection<Ticket> GetAssignedTickets(string? userId) {
        if (userId is null) {
            throw new ArgumentNullException();
        }

        var tickets = _ticketRepository.GetList(t => t.AssignedToUserId == userId);
        return tickets;
    }

    public Ticket? GetTicket(int? ticketId) {
        if (ticketId is null) {
            throw new ArgumentNullException();
        }

        var ticket = _ticketRepository.Get(t => t.Id == ticketId);

        return ticket;
    }
    
    public void CreateTicket(Ticket? ticket) {
        _ticketRepository.Add(ticket);
    }

    public void UpdateTicket(Ticket? oldTicket,Ticket? updatedTicket, string? userId) {
        if (oldTicket is null && updatedTicket is null && userId is null) {
            throw new ArgumentNullException();
        }
        
        if (oldTicket?.Title != updatedTicket?.Title) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "Title",
                OldValue = oldTicket?.Title,
                NewValue = updatedTicket?.Title,
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }

        if (oldTicket?.Description != updatedTicket?.Description) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "Description",
                OldValue = oldTicket?.Description,
                NewValue = updatedTicket?.Description,
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }
        
        if (oldTicket?.ProjectId != updatedTicket?.ProjectId) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "ProjectId",
                OldValue = oldTicket?.ProjectId.ToString(),
                NewValue = updatedTicket?.ProjectId.ToString(),
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }
        
        if (oldTicket?.TicketTypeId != updatedTicket?.TicketTypeId) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "TicketTypeId",
                OldValue = oldTicket?.TicketTypeId.ToString(),
                NewValue = updatedTicket?.TicketTypeId.ToString(),
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }
        
        if (oldTicket?.TicketStatusId != updatedTicket?.TicketStatusId) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "TicketStatusId",
                OldValue = oldTicket?.TicketStatusId.ToString(),
                NewValue = updatedTicket?.TicketStatusId.ToString(),
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }
        
        if (oldTicket?.AssignedToUserId != updatedTicket?.AssignedToUserId) {
            TicketHistory th = new() {
                TicketId = oldTicket?.Id,
                Property = "AssignedToUserId",
                OldValue = oldTicket?.AssignedToUserId,
                NewValue = updatedTicket?.AssignedToUserId,
                Changed = DateTime.Now,
                UserId = userId
            };
            
            _ticketHistoryRepository.Add(th);
        }

        if (updatedTicket == null) return;
        updatedTicket.Updated = DateTime.Now;

        _ticketRepository.Update(updatedTicket);
        
    }

    public ICollection<TicketHistory> GetTicketHistory(int? ticketId) {
        if (ticketId is null) {
            throw new ArgumentNullException();
        }

        ICollection<TicketHistory> ticketHistory = _ticketHistoryRepository.GetList(th => th.TicketId == ticketId);

        return ticketHistory;
    }

    public List<Ticket> GetProjectManagerTickets(string? userId) {
        if (userId is null) {
            throw new NoNullAllowedException();
        }

        var projects = _projectUserRepository.GetList(pu => pu.UserId == userId).Select(p=>p.Project);

        return projects.SelectMany(project => project.Tickets).ToList();
    }

    public void Save() {
        _ticketRepository.Save();
    }
}
