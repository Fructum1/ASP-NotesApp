using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;

namespace ASP_NotesApp.DAL.Repository
{
    public class NotesRepository : INotesRepository
    {
        public void Create(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<Note> CreateAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<Note> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> GetNotes(string email)
        {
            throw new NotImplementedException();
        }

        public void Update(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
