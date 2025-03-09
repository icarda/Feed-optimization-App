namespace DataLibrary.Exceptions
{
    public class GeneralDomainException : Exception
    {
        public GeneralDomainException()
        { }

        public GeneralDomainException(string message)
            : base(message)
        { }

        public GeneralDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}