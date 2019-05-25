using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate.DomainEvents;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions;
using DddInPractice.Domain.SeedObjects;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class SnackMachine : AggregateRoot
    {
        private const int MaxAmountOfSlots = 10;

        public Money MoneyInside { get; private set; } = Money.None;
        public Money MoneyInTransaction { get; private set; } = Money.None;
        
        // nav prop with backing field
        public readonly List<Slot> _slots;
        public IEnumerable<Slot> Slots => _slots.AsReadOnly();

        #region ctors
        protected SnackMachine()
        {
            _slots = new List<Slot>();
        }

        public SnackMachine(Money money)
        {
            LoadMoney(money);
        }
        #endregion

        #region Money operations
        public void InsertMoney(Money money)
        {
            //if (!money.IsAcceptable())
            //{
            //    throw new CoinOrNoteIsNotAcceptableException();
            //}

            MoneyInTransaction += money;
        }
        public void RemoveMoney()
        {
            var moneyToReturn = MoneyInside.Allocate(
                MoneyInTransaction.Amount);
            MoneyInside -= moneyToReturn;
            SetToZeroAmountInTransaction();
        }
        public void LoadMoney(Money money) => MoneyInside += money;
        #endregion

        #region Snak operations
        public bool CanBuySnak(int position)
        {
            var slot = GetSlot(position);

            if (slot.GetItemsQuantity() < 0)
            {
                return false;
            }

            if (MoneyInTransaction.Amount < slot.GetItemPrice())
            {
                return false;
            }

            var chargeToAllocate = MoneyInTransaction.Amount - slot.GetItemPrice();
            if (!MoneyInside.CanAllocate(chargeToAllocate))
            {
                return false;
            }

            return true;
        }
        public void BuySnack(int position)
        {
            if (!CanBuySnak(position))
            {
                throw new CannotBuySnakException();
            }

            var slot = GetSlot(position);
            slot.SubtractOneSnak();

            var allocatedCharge = MoneyInside.Allocate(
                MoneyInTransaction.Amount - slot.GetItemPrice());
            MoneyInside -= allocatedCharge;
            MoneyInside += MoneyInTransaction;

            SetToZeroAmountInTransaction();

            AddDomainEvent(new SnackBoughtDomainEvent(
                slot.SnakType.Id, slot.GetItemPrice()));
        }
        #endregion

        #region Slot operations
        public bool CanAddSlot()
        {
            return _slots.Count <= MaxAmountOfSlots;
        }
        public void AddSlot(int position, int itemsQuantity, decimal itemPrice, int snackTypeId)
        {
            if (_slots.Count > MaxAmountOfSlots)
            {
                throw new NoPlaceForSlotAvailableException();
            }

            var existingSlot = _slots.Where(s => s.GetPosition() == position).FirstOrDefault();
            if (existingSlot == null)
            {
                var slot = new Slot(position, itemsQuantity, itemPrice, snackTypeId);
                _slots.Add(slot);
            }
            else
            {
                throw new SlotAlreadyExistsException();
            }
        }
        #endregion

        #region Implementation details
        private Slot GetSlot(int position) =>
            Slots.First(s => s.GetPosition() == position);
        private void SetToZeroAmountInTransaction()
        {
            MoneyInTransaction = Money.None;
        }
        #endregion       
    }
}
