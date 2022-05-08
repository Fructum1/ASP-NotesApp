using ASP_NotesApp.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните заголовок заметки")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Заполните тело заметки")]
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
    }
}
