using Microsoft.AspNetCore.Mvc;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers; 

public class SubmitterController : Controller {
    private readonly TicketBusinessLogic _ticketBll;
    private readonly UserBusinessLogic _userBll;

    // GET
    public SubmitterController(IUserRepository<ApplicationUser> userRepository, 
        IRepository<ProjectUser> projectUserRepository,
        IRepository<Ticket> ticketRepository,
        IRepository<TicketHistory> ticketHistoryRepository
        ) {
        _ticketBll = new TicketBusinessLogic(ticketRepository, ticketHistoryRepository, projectUserRepository);
        _userBll = new UserBusinessLogic(userRepository);
    }

    public async Task<IActionResult> Index() {
        var user = await _userBll.GetUserByName(User.Identity?.Name);
        List<Ticket> tickets = _ticketBll.GetOwnedTickets(user?.Id).ToList();
        return View(tickets);
    }
}
