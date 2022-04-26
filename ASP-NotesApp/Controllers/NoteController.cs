using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.Enum;
using ASP_NotesApp.Models.ViewModels.Note;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly NoteManagerService _noteManager;
        private readonly UserManagerService _userManager;

        public NoteController(NoteManagerService noteManager, UserManagerService userManager)
        {
            _noteManager = noteManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _noteManager.GetAllAsync();
            notes = notes.Where(n => n.Status == (int)StatusNote.Active).ToList();
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _noteManager.GetNoteAsync(id);


            return PartialView("_Edit",note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model);
            }

            try
            {
                await _noteManager.EditAsync(new DTO.NoteEditDTO()
                {
                    NoteBody = model.NoteBody,
                    Pined = model.Pined,
                    Status = model.Status,
                    Title = model.Title
                }, model.Id);
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Edit", model);
            }
        }

        public async Task Delete(int id)
        {
            await _noteManager.DeleteAsync(id);
        }
    }
}
