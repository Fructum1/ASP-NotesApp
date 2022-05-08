using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.DTO;
using System.Web.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ASP_NotesApp.Extensions.Exceptions;
using ASP_NotesApp.DTO.User;

namespace ASP_NotesApp.Services
{
    public class UserManagerService
    {
        protected readonly IGenericRepository<User> _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int CurrentUserId
        {
            get
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        public UserManagerService(IGenericRepository<User> userRepo, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = userRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterAsync(RegisterDTO registerInfo)
        {
            if (registerInfo != null && ! await UserExist(registerInfo.Email))
            {
                var user = new User();
                user.Name = registerInfo.Name;
                user.Surname = registerInfo.Surname;
                user.Email = registerInfo.Email;
                user.Password = Crypto.HashPassword(registerInfo.Password);
                if (registerInfo.Patronymic != null) user.Patronymic = registerInfo.Patronymic;

                await _usersRepository.CreateAsync(user);
            }
            else
            {
                throw new UserRegisterException();
            }
        }

        public async Task LoginAsync(LoginDTO loginInfo, HttpContext context)
        {
            var user = await _usersRepository.GetByAttributeAsync(loginInfo.Email);
            if(user != null && Crypto.VerifyHashedPassword(user.Password, loginInfo.Password))
            {
                await Authenticate(user, context);
            }
            else
            {
                throw new Exception("Некорректные логин и(или) пароль");
            }
        }

        public async Task Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<int> GetCurrentUserId(string email)
        {
            var user = await _usersRepository.GetByAttributeAsync(email);
            if (user == null)
            {
                throw new UserNotFoundOrDeletedException();
            }

            return user.Id;
        }

        public async Task EditAsync(UserEditDTO model, int id)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user.Email == model.Email)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Patronymic = model.Patronymic;

                _usersRepository.Update(user);
            }
            else if(!(await UserExist(model.Email)))
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Patronymic = model.Patronymic;

                _usersRepository.Update(user);
            }
            else
            {
                throw new UserWithThisEmailAlreadyExistsException();
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            User user = await _usersRepository.GetAsync(id);
            if (user == null)
            {
                throw new UserNotFoundOrDeletedException();
            }

            return user;
        }

        private async Task Authenticate(User user, HttpContext context)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            }, "ApplicationCookie");

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims));
        }

        private async Task<bool> UserExist(string email)
        {
            var user = await _usersRepository.GetByAttributeAsync(email);
            if (user != null) return true;
            else return false;
        }
    }
}
