using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class NoPlaceForSlotAvailableException : Exception
    {
        public NoPlaceForSlotAvailableException()
        {
        }

        public NoPlaceForSlotAvailableException(string message) : base(message)
        {
        }

        public NoPlaceForSlotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPlaceForSlotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
