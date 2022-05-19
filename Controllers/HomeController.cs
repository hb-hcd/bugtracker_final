using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;
using SD_125_BugTracker.Notification;
using System.Diagnostics;

namespace SD_125_BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            if ( User.IsInRole("Admin") )
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ( User.IsInRole("Project Manager") )
            {
                return RedirectToAction("Index", "ProjectManager");
            }
            else if ( User.IsInRole("Developer") )
            {
                return RedirectToAction("Index", "Developers");
            }
            else if ( User.IsInRole("Submitter") )
            {
                return RedirectToAction("Index", "Submitter");
            }
            else
            {
                ViewBag.Message = "Sorry! You have no permission to access this page. Please contact the Administrator.";
            }
            return View();
        }
        
        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Email(string body)
        {
            EmailNotification _email = new();
            ViewBag.EmailStatus = await _email.TicketAssignEmail("bugtrackersd125@gmail.com", "Bug Tracker", "Test Ticket Assign Email", "This is a test for sending email notifications", "Test Project", "Information Request", "Low");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}