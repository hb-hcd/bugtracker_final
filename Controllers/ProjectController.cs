using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD_125_BugTracker.BLL;
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers
{
    
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Project Manager")]
    public class ProjectController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private ProjectBusinessLogic projectBL;
        private ProjectUserBusinessLogic projectUserBL;
        private AssignedProjectBusinessLogic assignedProjectBL;
        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
            projectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            projectUserBL = new ProjectUserBusinessLogic(new ProjectUserRepository(context));
            assignedProjectBL = new AssignedProjectBusinessLogic(new AssignedProjectRepository(context));
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
                return RedirectToAction("Index", "Admin");
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
            catch ( Exception ex )
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
            var userProjects = projectBL.GetUserProjects(projectIds).Where(p => p.IsArchived == false).ToList();
            ViewBag.userId = userId;
            return View(userProjects);
        }

        public IActionResult viewAllProjects(string userId)
        {
            List<Project> allProjects = projectBL.GetAllProjects().Where(p=>p.IsArchived == false).ToList();
            ViewBag.userId = userId;
            return View(allProjects);
        }


        public async Task<ActionResult> Assign(int ProjectId)
        {
            Project project = projectBL.Get(ProjectId);
            if ( project == null )
            {
                return NotFound("Project not found");
            }
            List<ApplicationUser> allusers = _userManager.Users.ToList();
            SelectList allusersSelectList = new SelectList(allusers, "Id", "UserName");
            ViewBag.ProjectId = project.Id;
            ViewBag.ProjectName = project.Name;
            //current assigned user
             AssignedProject projectToAssign = assignedProjectBL.Get(ProjectId);
            if ( projectToAssign != null )
            {
                ApplicationUser currAssignedUser = await _userManager.FindByIdAsync(projectToAssign.UserId);
                ViewBag.currentAssigned = currAssignedUser.UserName;
            }           
                return View(allusersSelectList);                  
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Assign(int projectId, string userId)
        {
            AssignedProject assignedProject = new AssignedProject();
            assignedProject.ProjectId = projectId;
            assignedProject.UserId = userId;
            assignedProjectBL.Add(assignedProject);
            TempData["message"] = "Assigned project successfully";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Unassign(int ProjectId)
        {
           AssignedProject assignedProject  = assignedProjectBL.Get(ProjectId);
            if ( assignedProject != null )
            {
             ApplicationUser currAssignedUser =await _userManager.FindByIdAsync(assignedProject.UserId);
             ViewBag.userName = currAssignedUser.UserName;
            }
            
           Project projectToUnassign = projectBL.Get(ProjectId);
            ViewBag.ProjectId = projectToUnassign.Id;

            return View(projectToUnassign);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Unassign(int ProjectId, string userName)
        {
            assignedProjectBL.Delete(ProjectId);
            TempData["message"] = "Unassigned project successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int ProjectId)
        {
            Project projectToEdit = projectBL.Get(ProjectId);
            return View(projectToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int Id, string Name)
        {
            Project projectToEdit = projectBL.Get(Id);
            projectToEdit.Name = Name;
            projectBL.Edit(projectToEdit);
            TempData["message"] = "Edited project successfully";
            return RedirectToAction("Index");
         
        }

        public IActionResult Archive(int ProjectId)
        {
            Project projectToArchive = projectBL.Get(ProjectId);
            projectToArchive.IsArchived = true;
            projectBL.Edit(projectToArchive);
            return RedirectToAction("Index");
        }
    }
}
