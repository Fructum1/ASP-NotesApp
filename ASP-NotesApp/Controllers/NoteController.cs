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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotesList(string? attribute)
        {
            var notes = await _noteManager.GetAllAsync();
            notes = notes.Where(n => n.Status == (int)StatusNote.Active);

            if (!String.IsNullOrEmpty(attribute))
            {
                notes = notes.Where(n => n?.Title == attribute || n?.NoteBody == attribute);
            }

            return PartialView("_GetNotesList", notes);
        }

        public async Task<IActionResult> Archived(string attribute)
        {
            var notes = await _noteManager.GetAllAsync();
            notes = notes.Where(n => n.Status == (int)StatusNote.Archived);

            if (!String.IsNullOrEmpty(attribute))
            {
                notes = notes.Where(n => n?.Title == attribute || n?.NoteBody == attribute);
            }

            return View(notes);
        }

        public async Task<IActionResult> TrashCan()
        {
            var notes = await _noteManager.GetAllAsync();
            notes = notes.Where(_n => _n.Status == (int)StatusNote.Deleted);

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create");
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
            try
            {
                await _noteManager.DeleteAsync(id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task Archive(int id)
        {
            try
            {
                await _noteManager.ArchiveAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task UnArchive(int id)
        {
            try
            {
                await _noteManager.UnArchiveAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task DeleteFromTrashCan(int id)
        {
            try
            {
                await _noteManager.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task RecoverFromTrashCan(int id)
        {
            try
            {
                await _noteManager.RecoverFromTrashCan(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
    }
}
