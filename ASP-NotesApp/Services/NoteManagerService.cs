using ASP_NotesApp.DAL;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.Enum;

namespace ASP_NotesApp.Services
{
    public class NoteManagerService
    {
        protected readonly IGenericRepository<Note> _noteRepository;
        private readonly UserManagerService _userManager;

        public NoteManagerService(IGenericRepository<Note> repo, UserManagerService userManager)
        {
            _userManager = userManager;
            _noteRepository = repo;
        }

        public async Task<Note> CreateAsync(NoteCreateDTO model)
        {
            Note note = new Note();
            note.Id = model.Id;
            note.Title = model.Title;
            note.NoteBody = model.NoteBody;
            note.CreationDate = DateTime.Now;
            note.Status = (int)StatusNote.Active;
            note.UserId = _userManager.CurrentUserId;
            note.Pined = model.Pined;

            if (NoteValid(note))
            {
                return await _noteRepository.CreateAsync(note);
            }

            else throw new Exception();
        }

        public async Task CreateDefault(string email)
        {
            Note note = new Note(){
                UserId = await _userManager.GetLastUserId(email),
                NoteBody = "Hello!",
                Title = "Welcome to my app",
                CreationDate = DateTime.Now,
                Pined = true,
                Status = (int)StatusNote.Active,
            };

            await _noteRepository.CreateAsync(note);
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            Note note = await _noteRepository.GetAsync(id);
            if (note == null) 
            { 
                throw new Exception(); 
            }

            return note;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var notes = await _noteRepository.Get(_userManager.CurrentUserId);

            return notes;
        }

        public async Task EditAsync(NoteEditDTO model, int id)
        {
            var note = await _noteRepository.GetAsync(id); 
            if (model.Status != (int)StatusNote.Deleted && note != null)
            {
                note.Title = model.Title;
                note.NoteBody = model.NoteBody;
                note.Pined = model.Pined;

                _noteRepository.Update(note);
            }
            else
            {
                throw new Exception();
            }
        }   

        public async Task ArchiveAsync(int id)
        {
            var note = await GetNoteAsync(id);

            if (note.Status != (int)StatusNote.Archived) 
            { 
                note.Status = (int)StatusNote.Archived; 
            }

            _noteRepository.Update(note);
        }

        public async Task DeleteAsync(int id)
        {
            var note = await GetNoteAsync(id);

            if (note.Status != (int)StatusNote.Deleted) 
            { 
                note.Status = (int)StatusNote.Deleted; 
            }

            _noteRepository.Update(note);
        }

        public async Task RemoveAsync(int id)
        {
            var note = await GetNoteAsync(id);

            if (note.Status == (int)StatusNote.Deleted)
            {
                await _noteRepository.DeleteAsync(id);
            }
        }

        private bool NoteValid(Note note)
        {
            if (note.Status == (int)StatusNote.Active &&   
               (note.NoteBody != null || note.Title != null))
                return true;
            else return false;
        }
    }
}
