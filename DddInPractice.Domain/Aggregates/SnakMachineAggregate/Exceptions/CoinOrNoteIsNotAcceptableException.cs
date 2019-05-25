using System;
using System.Runtime.Serialization;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions
{
    public class CoinOrNoteIsNotAcceptableException :
        Exception
    {
        public CoinOrNoteIsNotAcceptableException()
        {
        }

        public CoinOrNoteIsNotAcceptableException(string message) : base(message)
        {
        }

        public CoinOrNoteIsNotAcceptableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CoinOrNoteIsNotAcceptableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
