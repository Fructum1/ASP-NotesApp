using ASP_NotesApp.DAL;
using ASP_NotesApp.DAL.Repository;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Extensions.Exceptions;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Services;
using Xunit;

namespace ASP_NotesApp.Tests
{
    public class UserAuthServiceTests
    {
        [Fact]
        public async Task Register()
        {
            RegisterDTO registerDTO = new RegisterDTO();
            registerDTO.Email = "12@gmail.com";
            registerDTO.Surname = "Evgen";
            registerDTO.Id = 1;
            registerDTO.Password = "123123f";
            registerDTO.ConfirmPassword = "123123f";
            IGenericRepository<User> usersRepository = new UserRepository();
            //UserAuthService service = new UserAuthService(usersRepository);

           // Action act = () => service.Register(registerDTO);

            UserRegisterException exception = Assert.Throws<UserRegisterException>(act);
            Assert.Equal("Пользователь уже существует", exception.Message);
        }
    }
}
