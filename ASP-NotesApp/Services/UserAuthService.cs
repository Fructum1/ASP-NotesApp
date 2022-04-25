using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Extensions.Exceptions;

namespace ASP_NotesApp.Services
{
    public class UserAuthService
    {
        protected readonly IGenericRepository<Note> usersRepository;         

        public UserAuthService(IGenericRepository<Note> repo)
        {
            usersRepository = repo;
        }

        public async Task Register(RegisterDTO registerInfo)
        {
            throw new UserRegisterException();
        }
        public async Task<Note> GetNotesAsync()
        {
            Note note;

            note = await usersRepository.GetAsync(1);
                if (note == null)
                    throw new Exception();
            return note;
        }
    }
}
