using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NotesApp.Models.Domain
{
    [Table("User")]
    public class User
    {
        public User()
        {
            Notes = new HashSet<Note>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;


        public virtual ICollection<Note> Notes { get; set;}
    }
}
