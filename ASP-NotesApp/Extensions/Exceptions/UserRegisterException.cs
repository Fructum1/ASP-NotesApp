namespace ASP_NotesApp.Extensions.Exceptions
{
    public class UserRegisterException : Exception
    {
        public UserRegisterException() : base("Пользователь уже существует")
        {

        }
    }
}
