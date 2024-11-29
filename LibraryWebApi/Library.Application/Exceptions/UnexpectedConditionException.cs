namespace Library.Application.Exceptions
{
    public class UnexpectedConditionException : Exception
    {
        public UnexpectedConditionException(string message) : base(message) { }
    }
}
