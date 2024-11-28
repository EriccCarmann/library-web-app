namespace Library.Application.Exceptions
{
    public class InvalidPassworException : Exception
    {
        public InvalidPassworException(string message) : base(message) {}
    }
}
