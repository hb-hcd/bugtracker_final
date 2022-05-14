using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private ProjectBusinessLogic projectBL;
        private ProjectUserBusinessLogic projectUserBL;
       
        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
            projectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            projectUserBL = new ProjectUserBusinessLogic(new ProjectUserRepository(context));
        }
        public async Task<ActionResult> Index()
        {
         var currUsername = User.Identity.Name;
         ApplicationUser currUser = await _userManager.FindByNameAsync(currUsername);
            if ( currUser != null )
            {
                ViewBag.userId = currUser.Id;
                return View();
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
        }

        public IActionResult createProject(string userId)
        {
            ViewBag.userId = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> createProject(string userId, IFormCollection collection)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                Project project = new Project();
                ProjectUser projectUser = new ProjectUser();
                project.Name = collection["Name"].ToString();
                project.User = user;
                projectBL.Add(project);
                //keep track of the user-project relationship
                projectUser.UserId = userId;
                projectUser.ProjectId = project.Id;
                projectUserBL.Add(projectUser);

                TempData["message"] = "Project created succesfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex )
            {
                return NotFound(ex.Message);
            }  
       }

        public IActionResult viewProjects(string userId)
        {
            //get all projectIds of the current user
           var projectUsers = projectUserBL.GetList(userId);
            List<int> projectIds = new List<int>();
            foreach ( var projectUser in projectUsers )
            {
                     projectIds.Add((int)projectUser.ProjectId);
            }           
            var userProjects=  projectBL.GetUserProjects(projectIds);
          
            return View(userProjects);
        }

        public IActionResult viewAllProjects()
        {
            List<Project> allProjects = projectBL.GetAllProjects();
            return View(allProjects);
        }

        public IActionResult Assign()
        {
          List<ApplicationUser> allusers = _userManager.Users.ToList();
        }

    }
}
