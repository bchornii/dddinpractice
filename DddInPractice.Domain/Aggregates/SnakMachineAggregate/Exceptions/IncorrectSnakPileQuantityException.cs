using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class IncorrectSnakPileQuantityException : Exception
    {
        public IncorrectSnakPileQuantityException()
        {
        }

        public IncorrectSnakPileQuantityException(string message) : base(message)
        {
        }

        public IncorrectSnakPileQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectSnakPileQuantityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
