using ASP_NotesApp.Models.ViewModels;
using ASP_NotesApp.Models.ViewModels.User;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManagerService _userManager;
        private readonly NoteManagerService _noteManager;

        public UserController(UserManagerService userAuth, NoteManagerService noteManager)
        {
            _userManager = userAuth;
            _noteManager = noteManager;
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
                await _userManager.RegisterAsync(new DTO.RegisterDTO()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                });

                await _noteManager.CreateDefault(model.Email);
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
            if (User.Identity.IsAuthenticated && User != null) 
            { 
                return Redirect("/Note/Index"); 
            }

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
                await _userManager.LoginAsync(new DTO.LoginDTO() {
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                }, HttpContext);
                return Redirect("/Note/Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userManager.Logout(HttpContext);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(id);

            return View("Edit", user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UserEditViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _userManager.EditAsync(new DTO.User.UserEditDTO()
                {
                    Email = model.Email,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic
                }, model.Id);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
