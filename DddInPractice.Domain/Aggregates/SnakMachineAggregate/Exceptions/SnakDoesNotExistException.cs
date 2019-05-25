using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class SnakDoesNotExistException : Exception
    {
        public SnakDoesNotExistException()
        {
        }

        public SnakDoesNotExistException(string message) : base(message)
        {
        }

        public SnakDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SnakDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
