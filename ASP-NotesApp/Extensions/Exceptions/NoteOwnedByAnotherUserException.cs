namespace ASP_NotesApp.Extensions.Exceptions
{
    public class NoteOwnedByAnotherUserException : Exception
    {
        public NoteOwnedByAnotherUserException() : base("Заметка не принадлежит Вам")
        {

        }
    }
}
