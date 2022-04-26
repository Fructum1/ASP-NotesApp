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
            note.Creator = 1;
            note.Pined = model.Pined;

            if (NoteValid(note))
            {
                return await _noteRepository.CreateAsync(note);
            }

            else throw new Exception();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            Note note = await _noteRepository.GetAsync(id);
            if (note == null) throw new Exception();

            return note;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var notes = await _noteRepository.Get(_userManager.CurrentUserId);

            return notes;
        }

        public async Task EditNoteAsync(Note note)
        {
            if (note.Status != (int)StatusNote.Deleted &&
               note.Status != (int)StatusNote.Archived) ;
        }

        public async Task ArchiveAsync(int id)
        {
            //var note = await GetNoteAsync(id);
            //if()
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
