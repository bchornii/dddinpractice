using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class IncorrectSnakPilePriceException : Exception
    {
        public IncorrectSnakPilePriceException()
        {
        }

        public IncorrectSnakPilePriceException(string message) : base(message)
        {
        }

        public IncorrectSnakPilePriceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectSnakPilePriceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
