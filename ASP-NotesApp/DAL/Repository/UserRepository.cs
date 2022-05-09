using ASP_NotesApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_NotesApp.DAL.Repository
{
    public class UserRepository : IGenericRepository<User>
    {
        private readonly NoteAppDBContext _context;
        public UserRepository(NoteAppDBContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public async Task<User> CreateAsync(User item)
        {
            var result = await _context.Users.AddAsync(item);
            _context.SaveChanges();
            return result.Entity;
        }

        public void Delete(int id)
        {
            User userToDelete = Get(id);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            _context.Users.RemoveRange(_context.Users.ToList());
        }

        public async Task<User> DeleteAsync(int id)
        {
            User userToDelete = await GetAsync(id);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }

            return userToDelete;
        }

        public async Task<IEnumerable<User>> GetAll(int id)
        {
            return await _context.Users.Include(u => u.Notes).Where(u => u.Id != id).ToListAsync();
        }

        public User Get(int id)
        {
            return _context.Users.Include(u => u.Notes).FirstOrDefault(u => u.Id == id);
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByAttributeAsync(string attribute)
        {
            return await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Email == attribute);
        }

        public void Update(User item)
        {
            _context.Users.Update(item);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<User> items)
        {
            _context.Users.RemoveRange(items);
        }
    }
}
