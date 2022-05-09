using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class EditViewModel
    {
        private EditViewModel(Domain.Note note) 
        { 
            Id = note.Id;
            Title = note.Title;
            NoteBody = note.NoteBody;
            Pined = note.Pined;
            Status = note.Status;
        }
        public static EditViewModel FromNote(Domain.Note note)
        {
            return new EditViewModel(note);
        }
        public EditViewModel() { }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
