using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class EditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "123")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "123")]
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
