using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.ViewModels.Note;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteManagerService _noteManager;
        private readonly UserManagerService _userManager;

        public NoteController(NoteManagerService noteManager, UserManagerService userManager)
        {
            _noteManager = noteManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var notes = _noteManager.GetAllAsync();
            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _noteManager.CreateAsync(new DTO.NoteCreateDTO()
                {
                   Id = model.Id,
                   CreationDate = DateTime.Now,
                   NoteBody = model.NoteBody,
                   Title = model.Title,
                   Pined = model.Pined
                });
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
