namespace Library.Domain.Exceptions
{
    public class BookTakenException : Exception
    {
        public BookTakenException(string message) : base(message) { }
    }
}
