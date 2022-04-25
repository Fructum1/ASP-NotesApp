using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
