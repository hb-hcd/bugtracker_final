using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers
{
    [Authorize(Roles = "Project Manager")]
    public class ProjectManagerController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ProjectManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()          
        {     
            List<ApplicationUser> users = _userManager.Users.ToList();
            
            return View(users);
        }

        public async Task<ActionResult> UserRoleDetails(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if ( user != null )
            {
                var rolesOfUser = await _userManager.GetRolesAsync(user);
                List<IdentityRole> currentRoles = _db.Roles.Where(r => rolesOfUser.Contains(r.Name)).ToList();
                ViewBag.username = user.UserName;
                return View(currentRoles);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        public async Task<ActionResult> AssignRoleToUser(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if ( user != null )
            {
                ViewBag.UserName = user.UserName;
                ViewBag.UserId = user.Id;
                var rolesOfUser = await _userManager.GetRolesAsync(user);
                List<IdentityRole> allRoles = _db.Roles.ToList();
                List<IdentityRole> rolesToAssign = allRoles.Where(a => !rolesOfUser.Contains(a.Name)).ToList();
                SelectList rolesSlectList = new SelectList(rolesToAssign, "Id", "Name");
                return View(rolesSlectList);

            }
            else
            {
                return NotFound("User is not found");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignRoleToUser(string userId, string roleId)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                IdentityRole role = await _roleManager.FindByIdAsync(roleId);

                await _userManager.AddToRoleAsync(user, role.Name);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Assigned role successfully";
                return RedirectToAction(nameof(UserList));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> UnassignRole(string userId)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if ( user != null )
                {
                    ViewBag.UserName = user.UserName;
                    ViewBag.UserId = user.Id;
                    var rolesOfUser = await _userManager.GetRolesAsync(user);
                    List<IdentityRole> rolesToUnassign = _db.Roles.Where(r => rolesOfUser.Contains(r.Name)).ToList();
                    SelectList rolesSlectList = new SelectList(rolesToUnassign, "Id", "Name");
                    return View(rolesSlectList);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnassignRole(string userId, string roleId)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                IdentityRole role = await _roleManager.FindByIdAsync(roleId);

                await _userManager.RemoveFromRoleAsync(user, role.Name);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Unassigned role successfully";
                return RedirectToAction(nameof(UserList));
            }
            catch
            {
                return View();
            }

        }
    }
}
