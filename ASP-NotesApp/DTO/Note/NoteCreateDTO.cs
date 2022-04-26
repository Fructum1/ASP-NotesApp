namespace ASP_NotesApp.DTO
{
    public class NoteCreateDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public DateTime CreationDate { get; set; }
        public int Creator { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
