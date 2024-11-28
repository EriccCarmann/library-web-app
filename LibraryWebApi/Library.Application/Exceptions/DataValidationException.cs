namespace Library.Application.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException(string message) : base(message) { }
    }
}
