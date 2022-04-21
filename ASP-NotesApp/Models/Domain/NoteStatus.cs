namespace ASP_NotesApp.Models.Domain
{
    public class NoteStatus
    {
        public NoteStatus()
        {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Note> Notes { get; set;}
    }
}
