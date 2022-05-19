using Microsoft.AspNetCore.Mvc;

namespace SD_125_BugTracker.Controllers; 

public class DevelopersController : Controller {
    public IActionResult Index() {
        return View();
    }
}