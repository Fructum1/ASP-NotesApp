namespace ASP_NotesApp.Extensions.Exceptions
{
    public class NoteNotFoundOrDeletedException : Exception
    {
        public NoteNotFoundOrDeletedException() : base("Заметка не найдена")
        {

        }
    }
}
