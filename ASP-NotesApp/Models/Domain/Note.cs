using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NotesApp.Models.Domain
{
    [Table("Note")]
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public DateTime CreationDate { get; set; }       
        public bool Pined { get; set; }
        public int Status { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
