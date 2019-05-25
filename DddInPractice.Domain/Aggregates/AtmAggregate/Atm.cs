using DddInPractice.Domain.Aggregates.AtmAggregate.Exceptions;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using DddInPractice.Domain.SeedObjects;

namespace DddInPractice.Domain.Aggregates.AtmAggregate
{
    public class Atm : AggregateRoot
    {
        private const decimal CommissionRate = 0.01m;

        // Backing field
        public decimal _moneyCharged;

        // Value object
        public Money MoneyInside { get; private set; } = Money.None;

        public bool CanTakeMoney(decimal amount)
        {
            if (amount <= 0m)
            {
                return false;
            }

            if (MoneyInside.Amount < amount)
            {
                return false;
            }

            if (!MoneyInside.CanAllocate(amount))
            {
                return false;
            }

            return true;
        }
        public void TakeMoney(decimal amount)
        {
            if (!CanTakeMoney(amount))
            {
                throw new CannotTakeMoneyException();
            }

            var allocatedMoney = MoneyInside.Allocate(amount);
            MoneyInside -= allocatedMoney;

            var commission = CalculateCommission(amount);
            _moneyCharged += amount + commission;

            // raise event BalanceChangedEvent(_moneyCharged);
        }
        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
        public decimal CalculateCommission(decimal amount)
        {
            var commission = amount * CommissionRate;
            var lessThanCent = commission % 0.01m;
            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }

            return commission;
        }
    }
}
