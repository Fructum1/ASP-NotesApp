namespace ASP_NotesApp.Models.Domain
{
    public class User
    {
        public User()
        {
            Notes = new HashSet<Note>();
            NotesNavigation = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual ICollection<Note> Notes { get; set;}
        public virtual ICollection<Note> NotesNavigation { get; set; }
    }
}
