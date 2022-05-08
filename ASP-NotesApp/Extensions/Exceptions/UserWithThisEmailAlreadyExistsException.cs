namespace ASP_NotesApp.Extensions.Exceptions
{
    public class UserWithThisEmailAlreadyExistsException : Exception
    {
        public UserWithThisEmailAlreadyExistsException() : base("Пользователь с такой почтой уже существует")
        {

        }
    }
}
