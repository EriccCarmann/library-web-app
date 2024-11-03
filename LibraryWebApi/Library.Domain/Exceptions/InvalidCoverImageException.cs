namespace Library.Domain.Exceptions
{
    public class InvalidCoverImageException : Exception
    {
        public InvalidCoverImageException(string message) : base(message) { }
    }
}
