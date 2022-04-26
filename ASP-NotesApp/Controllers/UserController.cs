using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.ViewModels;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManagerService _userAuth;
        private readonly NoteAppDBContext _context;

        public UserController(UserManagerService userAuth, NoteAppDBContext context)
        {
            _userAuth = userAuth;

            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userAuth.RegisterAsync(new DTO.RegisterDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                });
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userAuth.LoginAsync(new DTO.LoginDTO() {
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                }, HttpContext);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userAuth.Logout(HttpContext);

            return RedirectToAction("Index");
        }
    }
}
