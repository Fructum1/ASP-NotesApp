namespace ASP_NotesApp.DTO.User
{
    public class UserEditDTO
    {
        public UserEditDTO() { }
        private UserEditDTO(Models.ViewModels.User.UserEditViewModel user)
        {
            Name = user.Name;
            Email = user.Email;
            Patronymic = user.Patronymic;
            Surname = user.Surname;
        }
        public static UserEditDTO FromUserEditViewModel(Models.ViewModels.User.UserEditViewModel user)
        {
            return new UserEditDTO(user);
        }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Surname { get; set; } = null!;
    }
}
