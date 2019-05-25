using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class IncorrectSlotDataException : Exception
    {
        public IncorrectSlotDataException()
        {
        }

        public IncorrectSlotDataException(string message) : base(message)
        {
        }

        public IncorrectSlotDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectSlotDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
