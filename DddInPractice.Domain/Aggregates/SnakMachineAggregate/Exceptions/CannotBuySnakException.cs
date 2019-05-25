using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class CannotBuySnakException : Exception
    {
        public CannotBuySnakException()
        {
        }

        public CannotBuySnakException(string message) : base(message)
        {
        }

        public CannotBuySnakException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotBuySnakException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
