using Microsoft.AspNetCore.Mvc;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers; 

public class TicketCommentsController : Controller {
    private readonly TicketCommentBll _ticketCommentBll;
    private readonly TicketBusinessLogic _ticketBll;
    private readonly UserBusinessLogic _userBll;
    
    
    public TicketCommentsController(IRepository<TicketComment> ticketCommentRepository,
        IRepository<ProjectUser> projectUserRepository,
        IRepository<Ticket> ticketRepository,
        IRepository<TicketHistory> ticketHistoryRepository, IUserRepository<ApplicationUser> userRepository) {
        _ticketBll = new TicketBusinessLogic(ticketRepository, ticketHistoryRepository, projectUserRepository);
        _ticketCommentBll = new TicketCommentBll(ticketCommentRepository);
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
    public async Task<IActionResult> Create([Bind("Comment,TicketId")] TicketComment ticketComment) {
        var user =  await _userBll.GetUserByName(User.Identity?.Name);
        ticketComment.UserId = user?.Id;

        try {
            _ticketCommentBll.CreateComment(ticketComment);
            _ticketCommentBll.Save();
            return Redirect("Index");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}