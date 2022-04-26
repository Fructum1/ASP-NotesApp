using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NotesApp.Models.Domain
{
    [Table("NoteStatus")]
    public class NoteStatus
    {
        public NoteStatus()
        {
            Notes = new HashSet<Note>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Note> Notes { get; set;}
    }
}
