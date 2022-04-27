using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите почтовый ящик")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [Category("Security")]
        [PasswordPropertyText(true)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Повторите пароль")]
        [Category("Security")]
        [PasswordPropertyText(true)]      
        public string ConfirmPassword { get; set; } = null!;
    }
}
