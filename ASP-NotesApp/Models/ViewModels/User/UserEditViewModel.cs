using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.User
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Имя")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите почтовый ящик")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; } = null!;


        public UserEditViewModel() { }
        private UserEditViewModel(Domain.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Patronymic = user.Patronymic;
            Surname = user.Surname;
        }
        public static UserEditViewModel FromUser(Domain.User user)
        {
            return new UserEditViewModel(user);
        }
    }
}
