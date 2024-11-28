namespace Library.Application.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message) : base(message) { }
    }
}
