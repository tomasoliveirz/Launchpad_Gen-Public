namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidTokenomicException : Exception
    {
        public InvalidTokenomicException(string message) 
            : base(message)
        {
        }

        public InvalidTokenomicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}