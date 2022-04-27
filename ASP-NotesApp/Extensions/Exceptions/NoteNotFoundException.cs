namespace ASP_NotesApp.Extensions.Exceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException() : base("Заметка не найдена")
        {

        }
    }
}
