using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly UserAuthService userAuth;

        public NoteController(UserAuthService userAuth)
        {
            userAuth = userAuth;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
