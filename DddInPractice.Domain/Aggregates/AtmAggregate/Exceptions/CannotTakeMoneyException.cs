using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.AtmAggregate.Exceptions
{
    public class CannotTakeMoneyException : Exception
    {
        public CannotTakeMoneyException()
        {
        }

        public CannotTakeMoneyException(string message) : base(message)
        {
        }

        public CannotTakeMoneyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotTakeMoneyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
