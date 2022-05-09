using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using ASP_NotesApp.Extensions;

namespace ASP_NotesApp.DAL.Repository
{
    public class NotesRepository : IGenericRepository<Note>
    {
        private readonly NoteAppDBContext _context;
        public NotesRepository(NoteAppDBContext context)
        {
            _context = context;
        }
        public void Create(Note item)
        {
            _context.Notes.Add(item);
            _context.SaveChanges();
        }

        public async Task<Note> CreateAsync(Note item)
        {
            var result = await _context.Notes.AddAsync(item);
            _context.SaveChanges();
            return result.Entity;
        }

        public void Delete(int id)
        {
            Note noteToDelete = Get(id);

            if (noteToDelete != null)
            {
                _context.Notes.Remove(noteToDelete);
                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            _context.Notes.RemoveRange(_context.Notes.ToList());
        }

        public async Task<Note> DeleteAsync(int id)
        {
            Note noteToDelete = await GetAsync(id);

            if (noteToDelete != null)
            {
                _context.Notes.Remove(noteToDelete);
                _context.SaveChanges();
            }
            return noteToDelete;
        }

        public void DeleteRange(IEnumerable<Note> items)
        {
            _context.Notes.RemoveRange(items);
        }

        public async Task<IEnumerable<Note>> GetAll(int id)
        {
            return await _context.Notes.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task<Note> GetAsync(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Note> GetByAttributeAsync(string attribute)
        {
            return await _context.Notes.Where(n => n.Title.Contains(attribute) || n.NoteBody.Contains(attribute)).FirstAsync();
        }

        public Note Get(int id)
        {
            return _context.Notes.FirstOrDefault(u => u.Id == id);
        }

        public void Update(Note item)
        {
            _context.Notes.Update(item);
            _context.SaveChanges();
        }
    }
}
