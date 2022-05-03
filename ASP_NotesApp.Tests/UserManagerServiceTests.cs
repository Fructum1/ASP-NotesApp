using ASP_NotesApp.DAL;
using ASP_NotesApp.DAL.Repository;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Extensions.Exceptions;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Web.Helpers;
using Xunit;

namespace ASP_NotesApp.Tests
{
    public class UserManagerServiceTests
    {
        public Mock<IGenericRepository<User>> _mockUserRepository = new Mock<IGenericRepository<User>>();
        public Mock<IHttpContextAccessor> _mockIhttpContextAccessor = new Mock<IHttpContextAccessor>();

        [Fact]
        public async void EditAsyncSuccess()
        {
            User userToUpdate = GetUserTest(1);
            _mockUserRepository.Setup(repo => repo.GetAsync(1).Result).Returns(userToUpdate);
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);

            await _userManager.EditAsync(new DTO.User.UserEditDTO()
            {
                Name = "Segei",
                Email = "fracss@mail.ru",
                Patronymic = "Gos",
                Surname = "Fa",
            }, 1);
            var excepted = new User()
            {
                Id = 1,
                Name = "Segei",
                Email = "fracss@mail.ru",
                Patronymic = "Gos",
                Surname = "Fa",
                Password = "123",
            };

            Assert.Equal(JsonConvert.SerializeObject(excepted), JsonConvert.SerializeObject(userToUpdate));
        }
        [Fact]
        public void EditAsyncThrowException()
        {
            User userToUpdate = GetUserTest(1);
            _mockUserRepository.Setup(repo => repo.GetAsync(1).Result).Returns(userToUpdate);
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>()));
            _mockUserRepository.Setup(r => r.GetByAttributeAsync(userToUpdate.Email).Result).Returns(userToUpdate);
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);

            Assert.ThrowsAsync<UserNotFoundOrDeletedException>(async () => await _userManager.EditAsync(new DTO.User.UserEditDTO()
            {
                Name = "Segei",
                Email = "fracss@mail.ru",
                Patronymic = "Gos",
                Surname = "Fa",
            }, 1));
        }
        [Fact]
        public async void RegisterAsyncSuccess()
        {
            User user = new User() { Id = 1, Email = "fructum@gmail.com", Name = "Evgenii", Password = Crypto.HashPassword("123"), Surname = "Arg" };
            RegisterDTO registerDTO = new RegisterDTO() { Email = "fructum@gmail.com", Name = "Evgenii", Password = Crypto.HashPassword("123"), Surname = "Arg" };
            _mockUserRepository.Setup(r => r.CreateAsync(It.IsAny<User>()));
            _mockUserRepository.Setup(r => r.GetAsync(1).Result).Returns(user);
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);

            await _userManager.RegisterAsync(registerDTO);
            var except = await _userManager.GetUserAsync(1);

            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(except));
        }
        [Fact]
        public void RegisterAsyncUserExist()
        {
            User user = new User() { Id = 1, Email = "fructum@gmail.com", Name = "Evgenii", Password = Crypto.HashPassword("123"), Surname = "Arg" };
            RegisterDTO registerDTO = new RegisterDTO() { Email = "fructum@gmail.com", Name = "Evgenii", Password = Crypto.HashPassword("123"), Surname = "Arg" };
            _mockUserRepository.Setup(r => r.CreateAsync(It.IsAny<User>()));
            _mockUserRepository.Setup(r => r.GetAsync(1).Result).Returns(user);
            _mockUserRepository.Setup(r => r.GetByAttributeAsync(registerDTO.Email).Result).Returns(user);
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);

            Assert.ThrowsAsync<UserRegisterException>(async () => await _userManager.RegisterAsync(registerDTO));
        }

        [Fact]
        public void GetUserAsyncSuccess()
        {
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);
            _mockUserRepository.Setup(repo => repo.GetAsync(1).Result).Returns(GetUserTest(1));

            // Act
            var result = _userManager.GetUserAsync(1);
            var excepted = GetUserTest(1);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(excepted), JsonConvert.SerializeObject(result.Result));
        }
        [Fact]
        public void GetUserAsyncFailure()
        {
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);
            _mockUserRepository.Setup(repo => repo.GetAsync(1).Result).Returns(GetUserTest(1));

            // Act
            var result = _userManager.GetUserAsync(1);
            var excepted = GetUserTest(4);

            // Assert
            Assert.NotEqual(JsonConvert.SerializeObject(excepted), JsonConvert.SerializeObject(result.Result));
        }
        [Fact]
        public void GetUserAsyncThrowException()
        {
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContextAccessor.Object);
            _mockUserRepository.Setup(repo => repo.GetAsync(1).Result).Returns(GetUserTest(1));

            Assert.ThrowsAsync<UserNotFoundOrDeletedException>(async () => await _userManager.GetUserAsync(4));
        }


        private User GetUserTest(int id)
        {
            User user = new User()
            {
                Name = "Evgenii",
                Email = "fracs@mail.ru",
                Id = id,
                Password = "123",
                Patronymic = "Gas",
                Surname = "Fo",
            };
            return user;
        }
    }
}
