using Microsoft.AspNetCore.Mvc;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers; 

public class TicketAttachmentsController : Controller {
    private readonly TicketAttachmentBll _ticketAttachmentBll;
    private readonly TicketBusinessLogic _ticketBll;
    private readonly UserBusinessLogic _userBll;
    
    
    public TicketAttachmentsController(IRepository<TicketAttachment> ticketAttachmentRepository,
        IRepository<ProjectUser> projectUserRepository,
        IRepository<Ticket> ticketRepository,
        IRepository<TicketHistory> ticketHistoryRepository, IUserRepository<ApplicationUser> userRepository, TicketAttachmentBll ticketAttachmentBll) {
        _ticketAttachmentBll = new TicketAttachmentBll(ticketAttachmentRepository);
        _ticketBll = new TicketBusinessLogic(ticketRepository, ticketHistoryRepository, projectUserRepository);
        _userBll = new UserBusinessLogic(userRepository);
    }

    [HttpGet]
    public IActionResult Create(int? ticketId) {
        if (ticketId is null) {
            return BadRequest();
        }

        var ticket = _ticketBll.GetTicket(ticketId);

        if (ticket is null) {
            return NotFound();
        }

        ViewBag.ticket = ticket;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Comment,TicketId")] TicketAttachment ticketAttachment) {
        var user =  await _userBll.GetUserByName(User.Identity?.Name);
        ticketAttachment.UserId = user?.Id;

        try {
            _ticketAttachmentBll.CreateAttachment(ticketAttachment);
            _ticketAttachmentBll.Save();
            return Redirect("Index");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}