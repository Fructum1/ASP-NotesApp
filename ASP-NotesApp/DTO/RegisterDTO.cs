using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.DTO
{
    public class RegisterDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; } = null!;

        [StringLength(50, MinimumLength = 3)]
        public string? Patronymic { get; set; }

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
