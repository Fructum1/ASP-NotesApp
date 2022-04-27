namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
    }
}
