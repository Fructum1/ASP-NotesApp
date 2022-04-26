namespace ASP_NotesApp.DTO
{
    public class NoteEditDTO
    {
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
