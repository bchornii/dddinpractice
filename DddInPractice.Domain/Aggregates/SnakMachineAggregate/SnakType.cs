using DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions;
using DddInPractice.Domain.SeedObjects;
using System;
using System.Linq;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class SnakType : Enumeration
    {
        public static SnakType None = new SnakType(0, nameof(SnakType.None));
        public static SnakType Chocolate = new SnakType(1, nameof(SnakType.Chocolate));
        public static SnakType Soda = new SnakType(2, nameof(SnakType.Soda));
        public static SnakType Gum = new SnakType(3, nameof(SnakType.Gum));

        protected SnakType() { }

        public SnakType(int id, string name) : base(id, name)
        {

        }

        public static SnakType[] List() =>
            new[] { None, Chocolate, Soda, Gum };

        public static SnakType FromName(string name)
        {
            var snak = List().SingleOrDefault(s => s.Name
                .Equals(name, StringComparison.OrdinalIgnoreCase));

            if (snak == null)
            {
                throw new SnakDoesNotExistException(nameof(name));
            }
            return snak;
        }
    }
}
