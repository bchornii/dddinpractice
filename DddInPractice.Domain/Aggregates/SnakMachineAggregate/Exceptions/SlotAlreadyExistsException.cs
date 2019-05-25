using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class SlotAlreadyExistsException : Exception
    {
        public SlotAlreadyExistsException()
        {
        }

        public SlotAlreadyExistsException(string message) : base(message)
        {
        }

        public SlotAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SlotAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
