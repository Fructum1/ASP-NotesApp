using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;

namespace ASP_NotesApp.Services
{
    public class NoteManagerService
    {
        protected readonly IGenericRepository<Note> _noteRepository;

        public NoteManagerService(IGenericRepository<Note> repo)
        {
            _noteRepository = repo;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            retu
        }
    }
}
