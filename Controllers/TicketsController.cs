using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;
using Type = SD_125_BugTracker.Models.Type;

namespace SD_125_BugTracker.Controllers; 

[Authorize]
public class TicketsController : Controller {
    private readonly TicketBusinessLogic _ticketBll;
    private readonly ProjectBusinessLogic _projectBll;
    private readonly UserBusinessLogic _userBll;
    private readonly AssignedProjectBusinessLogic _assignedBll;
    public TicketsController(
        IUserRepository<ApplicationUser> userRepository,
        IRepository<Project> projectRepository,
        IRepository<ProjectUser> projectUserRepository,
        IRepository<Ticket> ticketRepository,
        IRepository<TicketHistory> ticketHistoryRepository,
        ApplicationDbContext context
    ) {
        _ticketBll = new TicketBusinessLogic(ticketRepository, ticketHistoryRepository, projectUserRepository);
        _userBll = new UserBusinessLogic(userRepository);
        _projectBll = new ProjectBusinessLogic(projectRepository);     
        _assignedBll = new AssignedProjectBusinessLogic(new AssignedProjectRepository(context));
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        List<Ticket> tickets = new();
        var user = await _userBll.GetUserByName(User.Identity?.Name);
        if (User.IsInRole("Admin")) {
            tickets =  _ticketBll.GetAllTickets().ToList();
        } else if (User.IsInRole("Project Manager")) {
            tickets = _ticketBll.GetProjectManagerTickets(user?.Id);
        } else if (User.IsInRole("Developer")) {
            tickets = _ticketBll.GetAssignedTickets(user?.Id).ToList();
        } else if (User.IsInRole("Submitter")) {
            tickets = _ticketBll.GetOwnedTickets(user?.Id).ToList();
        }

        return View(tickets);
    }

    [HttpGet]
    public IActionResult ProjectTickets(int? projectId) {
        if (projectId is null) {
            return BadRequest();
        }
        
        Project? project = _projectBll.Get(projectId);

        
        return View(project?.Tickets);
    }

    [HttpGet]
    public IActionResult History(int? ticketId) {
        if (ticketId is null) {
            return BadRequest();
        }
        
        var ticketHistory = _ticketBll.GetTicketHistory(ticketId);
        
        return View(ticketHistory);
    }

    [HttpGet]
    [Authorize(Roles = "Submitter")]
    public IActionResult Create(string userId) {
        //only getting projects assigned to the submitter
        var assignedProjects = _assignedBll.GetList(userId).ToList();
        List<int> projectIds = new List<int>();
        foreach ( var assignedProject in assignedProjects )
        {
            projectIds.Add(assignedProject.ProjectId);
        }
        var projectsBelongToUser = _projectBll.GetUserProjects(projectIds).Where(p => p.IsArchived == false).ToList();

        ViewBag.projects = new SelectList(projectsBelongToUser, "Id", "Name");
        var types = from Type t in Enum.GetValues(typeof(Type)) 
            select new {Id = (int)t, Name = t.ToString()};
        
        var priorities = from Priority t in Enum.GetValues(typeof(Priority)) 
            select new {Id = (int)t, Name = t.ToString()};
        
        
        ViewBag.ticketTypes = new SelectList(types.ToList(), "Id", "Name");
        ViewBag.ticketPriorities = new SelectList(priorities.ToList(), "Id", "Name");

        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "Submitter")]
    public async Task<IActionResult> Create([Bind("Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId")] Ticket ticket) {
        var user = await _userBll.GetUserByName(User.Identity?.Name);
        ticket.OwnerUserId = user?.Id;
        ticket.Created = DateTime.Now;
        ticket.TicketStatusId = (int?)Status.Open;

        try {
            _ticketBll.CreateTicket(ticket);
            _ticketBll.Save();
            return RedirectToAction(nameof(Details), new {id = ticket.Id});
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Project Manager")]
    public async Task<IActionResult> AssignUserToTicket() {
        var users = await _userBll.GetUsers();
        ViewBag.users = new SelectList(users.ToList(), "Id", "UserName");
        
        ViewBag.tickets = new SelectList(_ticketBll.GetAllTickets(), "Id", "Title");
        
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Project Manager")]
    public async Task<IActionResult> AssignUserToTicket(int? ticketId, string? userId) {

        var ticket = _ticketBll.GetTicket(ticketId);
        var user = await _userBll.GetUserById(userId);

        var newTicket = ticket.Copy();
        
        newTicket.AssignedToUserId = user?.Id;

        try {
            _ticketBll.UpdateTicket(ticket, newTicket, (await _userBll.GetUserByName(User.Identity?.Name))?.Id);
            _ticketBll.Save();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public IActionResult Details(int? id) {
        if (id is null) {
            BadRequest();
        }

        var ticket = _ticketBll.GetTicket(id);

        return View(ticket);
    }

    [HttpGet]
    public IActionResult Edit(int? id) {
        if (id is null) {
            return BadRequest();
        }

        Ticket ticket;
        
        try {
            ticket = _ticketBll.GetTicket(id);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

        var types = from Type t in Enum.GetValues(typeof(Type)) 
            select new {Id = (int)t, Name = t.ToString()};
        
        var priorities = from Priority t in Enum.GetValues(typeof(Priority)) 
            select new {Id = (int)t, Name = t.ToString()};
        
        ViewBag.ticketTypes = new SelectList(types.ToList(), "Id", "Name");
        ViewBag.ticketPriority = new SelectList(priorities.ToList(), "Id", "Name");

        return View(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, string? title, string? description, int? ticketTypeId, int? ticketPriorityId) {
        if (id is null) {
            return BadRequest();
        }
        
        try {
            var ticket = _ticketBll.GetTicket(id);

            var updatedTicket = ticket.Copy();
            
            updatedTicket.Title = title;
            updatedTicket.Description = description;
            updatedTicket.TicketTypeId = ticketTypeId;
            updatedTicket.TicketPriorityId = ticketPriorityId;

            var user = await _userBll.GetUserByName(User.Identity?.Name);
            
          _ticketBll.UpdateTicket(ticket, updatedTicket, user?.Id);
          _ticketBll.Save();

          return RedirectToAction(nameof(Details), new {id = ticket.Id});
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    public IActionResult ChangeStatus(int? id) {
        if (id is null) {
            return BadRequest();
        }

        Ticket ticket;
        
        try {
            ticket = _ticketBll.GetTicket(id);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

        var statuses = from Status t in Enum.GetValues(typeof(Status)) 
            select new {Id = (int)t, Name = t.ToString()};

        ViewBag.ticketStatuses = new SelectList(statuses.ToList(), "Id", "Name");

        return View(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(int? id, int? ticketStatusId) {
        if (id is null) {
            return BadRequest();
        }
        
        try {
            var ticket = _ticketBll.GetTicket(id);

            var updatedTicket = ticket.Copy();
            
            updatedTicket.TicketStatusId = ticketStatusId;

            var user = await _userBll.GetUserByName(User.Identity?.Name);
            
            _ticketBll.UpdateTicket(ticket, updatedTicket, user?.Id);
            _ticketBll.Save();

            return RedirectToAction(nameof(Details), new {id = ticket.Id});
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }


  
}
