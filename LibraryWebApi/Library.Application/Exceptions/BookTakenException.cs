namespace Library.Application.Exceptions
{
    public class BookTakenException : Exception
    {
        public BookTakenException(string message) : base(message) { }
    }
}
