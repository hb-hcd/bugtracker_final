using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
            List<ApplicationUser> users = _db.Users.ToList();
            return View(users);
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

    }
}
