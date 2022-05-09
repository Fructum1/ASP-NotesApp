namespace ASP_NotesApp.DTO
{
    public class NoteEditDTO
    {
        private NoteEditDTO(Models.ViewModels.Note.EditViewModel noteEdit)
        {
            Id = noteEdit.Id;
            Title = noteEdit.Title;
            NoteBody = noteEdit.NoteBody;
            Pined = noteEdit.Pined;
            Status = noteEdit.Status;
        } 
        public static NoteEditDTO FromEditViewModel(Models.ViewModels.Note.EditViewModel note)
        {
            return new NoteEditDTO(note);
        }
        public NoteEditDTO() { }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
