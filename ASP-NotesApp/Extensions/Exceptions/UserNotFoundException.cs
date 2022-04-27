namespace ASP_NotesApp.Extensions.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Пользователь не найден")
        {

        }
    }
}
