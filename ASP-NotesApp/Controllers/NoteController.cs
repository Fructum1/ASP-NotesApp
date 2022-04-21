using ASP_NotesApp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class NoteController : Controller
    {
        INotesRepository notesRepository;

        public NoteController(INotesRepository repository)
        {
            notesRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
