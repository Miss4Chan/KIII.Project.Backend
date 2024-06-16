using System;
using System.Runtime.Serialization;


namespace Domain.CustomExceptions
{
    public class InvalidEmailPasswordException : Exception
    {
        public InvalidEmailPasswordException()
        {
        }

        public InvalidEmailPasswordException(string message) : base(message)
        {
        }

        public InvalidEmailPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEmailPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}