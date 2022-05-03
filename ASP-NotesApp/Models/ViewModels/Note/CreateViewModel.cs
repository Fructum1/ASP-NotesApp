using ASP_NotesApp.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        //[CompareOneOfNull("NoteBody", ErrorMessage = "Заполните хотя бы одно поле")]
        public string? Title { get; set; }
        //[CompareOneOfNull("Title", ErrorMessage = "Заполните хотя бы одно поле")]
        [Required(ErrorMessage = "123")]
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
    }
}
