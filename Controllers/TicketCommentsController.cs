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
    public async Task<IActionResult> Index(int? ticketid)
    {
        if ( ticketid is null )
        {
            return NotFound("Ticket not found");
        }
        try
        {
            ViewBag.ticketId = ticketid;
            List<TicketComment> comments = _ticketCommentBll.GetTicketCommentsByTicketId(ticketid).ToList();
            return View(comments);
        }
        catch ( Exception e )
        {
            Console.WriteLine(e);
            throw;
        }
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
            return RedirectToAction("Index", new { ticketid = ticketComment.TicketId});
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }



    public ActionResult Delete(int commentId, int ticketId)
    {
       var comment = _ticketCommentBll.GetCommentById(commentId);
        _ticketCommentBll.DeleteComment(comment);
        _ticketCommentBll.Save();
        return RedirectToAction("Index", new
        {
            ticketid = ticketId
        });
    }
    [HttpGet]
    public ActionResult Edit(int commentId)
    {
        var comment = _ticketCommentBll.GetCommentById(commentId);
         return View(comment);
    }

    [HttpPost]
    public ActionResult Edit(int Id, string? Comment)
    {
        var comment = _ticketCommentBll.GetCommentById(Id);
        if ( Comment != null )
        {
            comment.Comment = Comment;
        }
        _ticketCommentBll.UpdateComment(comment);
        _ticketCommentBll.Save();
        return RedirectToAction("Index", new
        {
            ticketid = comment.TicketId
        });
    }

    [HttpGet]
    public ActionResult Details(int commentId)
    {
        var comment = _ticketCommentBll.GetCommentById(commentId);
        return View(comment);
    }
}
    