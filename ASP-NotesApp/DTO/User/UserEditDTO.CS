namespace ASP_NotesApp.DTO.User
{
    public class UserEditDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Surname { get; set; } = null!;
    }
}
