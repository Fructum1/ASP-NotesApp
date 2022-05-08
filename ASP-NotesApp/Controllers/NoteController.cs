using ASP_NotesApp.Models.Enum;
using ASP_NotesApp.Models.ViewModels.Note;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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

        public IActionResult Index(int? id = 0)
        {
            ViewBag.Status = id;
            return View();
        }

        public async Task<IActionResult> GetNotesList(string? attribute, int status = 0)
        {
            var notes = await _noteManager.GetAllAsync();

            switch (status)
            {
                case (int)StatusNote.Active:
                    notes = notes.Where(n => n.Status == (int)StatusNote.Active);
                    break;
                case (int)StatusNote.Archived:
                    notes = notes.Where(n => n.Status == (int)StatusNote.Archived);
                    break;
                case (int)StatusNote.Deleted:
                    notes = notes.Where(n => n.Status == (int)StatusNote.Deleted);
                    break;
            }

            if (!String.IsNullOrEmpty(attribute))
            {
                notes = notes.Where(n => (n.Title != null && n.Title.StartsWith(attribute)) || (n.NoteBody != null && n.NoteBody.StartsWith(attribute)));
            }

            return PartialView("_GetNotesList", notes);
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
                return PartialView("_Create",model);
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
                return PartialView("_Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Create",model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _noteManager.GetNoteAsync(id);

            return PartialView("_Edit",note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
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
                    Title = model.Title,
                }, model.Id);
                if (model.Status == (int)StatusNote.Archived)
                {
                    return RedirectToAction("Index", new {id = (int)StatusNote.Archived });
                }
                else
                {
                    return RedirectToAction("Index");
                }
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
                await _noteManager.RecoverFromTrashCanAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task PinNote(int id)
        {
            try
            {
                await _noteManager.PinAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        public async Task UnPinNote(int id)
        {
            try
            {
                await _noteManager.UnPinAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
    }
}
