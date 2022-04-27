using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.User
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите почтовый ящик")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; } = null!;
    }
}
