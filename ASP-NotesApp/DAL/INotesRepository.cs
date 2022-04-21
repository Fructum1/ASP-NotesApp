using ASP_NotesApp.Models.Domain;

namespace ASP_NotesApp.DAL
{
    public interface INotesRepository
    {
        void Create(Note note);
        Task<Note> CreateAsync(Note note);
        void Update(Note note);
        IEnumerable<Note> GetNotes(string email);
        void Delete(int id);
        Task<Note> DeleteAsync(int id);
        void DeleteAll();
    }
}
