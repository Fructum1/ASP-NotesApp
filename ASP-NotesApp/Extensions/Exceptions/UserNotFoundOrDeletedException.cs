namespace ASP_NotesApp.Extensions.Exceptions
{
    public class UserNotFoundOrDeletedException : Exception
    {
        public UserNotFoundOrDeletedException() : base("Пользователь не найден")
        {

        }
    }
}
