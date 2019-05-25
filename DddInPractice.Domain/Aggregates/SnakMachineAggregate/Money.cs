using DddInPractice.Domain.SeedObjects;
using System;
using System.Linq;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class Money : ValueObject<Money>
    {
        public static Money None = new Money(0, 0, 0, 0, 0);
        public static Money Cent = new Money(1, 0, 0, 0, 0);
        public static Money TenCent = new Money(0, 1, 0, 0, 0);
        public static Money Quarter = new Money(0, 0, 1, 0, 0);
        public static Money Dollar = new Money(0, 0, 0, 1, 0);
        public static Money FiveDollar = new Money(0, 0, 0, 0, 1);

        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }

        protected Money() { }

        public Money(int oneCentCount, int tenCentCount, int quarterCount,
            int oneDollarCount, int fiveDollarCount)
        {
            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
        }

        public decimal Amount =>
            OneCentCount * 0.01m +
            TenCentCount * 0.1m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5;
        public bool CanAllocate(decimal amount)
        {
            var money = Allocate(amount);
            return money.Amount == amount;
        }
        public Money Allocate(decimal amount)
        {
            var fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount -= fiveDollarCount * 5;

            var oneDollarCount = Math.Min((int)amount, OneCentCount);
            amount -= oneDollarCount;

            int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCount);
            amount -= quarterCount * 0.25m;

            int tenCentCount = Math.Min((int)(amount / 0.1m), QuarterCount);
            amount -= tenCentCount * 0.1m;

            int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);

            return new Money(oneCentCount, tenCentCount,
                quarterCount, oneDollarCount, fiveDollarCount);
        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount);
        }
        public static Money operator -(Money money1, Money money2)
        {
            return new Money(money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount);
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount &&
                TenCentCount == other.TenCentCount &&
                QuarterCount == other.QuarterCount &&
                OneDollarCount == other.OneDollarCount &&
                FiveDollarCount == other.FiveDollarCount;
        }
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hash = OneCentCount;
                hash = hash * 397 ^ TenCentCount;
                hash = hash * 397 ^ QuarterCount;
                hash = hash * 397 ^ OneDollarCount;
                hash = hash * 397 ^ FiveDollarCount;
                return hash;
            }
        }
    }
}
