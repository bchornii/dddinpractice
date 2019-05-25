using DddInPractice.Domain.Aggregates.SnakMachineAggregate.Exceptions;
using DddInPractice.Domain.SeedObjects;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public class Slot : Entity
    {
        private int _position;
        private int _itemsQuantity;
        private decimal _itemPrice;

        private int _snakTypeId;
        public SnakType SnakType { get; private set; }

        protected Slot() { }

        public Slot(int position, int itemsQuantity, decimal itemPrice, int snakTypeId)
        {
            _position = position > 0 ? position : throw new IncorrectSlotDataException(nameof(position));

            if (itemsQuantity < 0)
            {
                throw new IncorrectSlotDataException(nameof(itemsQuantity));
            }
            _itemsQuantity = itemsQuantity;

            if (itemPrice < 0 || itemPrice % 0.01m > 0)
            {
                throw new IncorrectSlotDataException(nameof(itemPrice));
            }
            _itemPrice = itemPrice;

            _snakTypeId = snakTypeId > 0 ? snakTypeId : throw new IncorrectSlotDataException(nameof(snakTypeId));
        }

        public int GetPosition()
        {
            return _position;
        }

        public decimal GetItemPrice()
        {
            return _itemPrice;
        }

        public int GetItemsQuantity()
        {
            return _itemsQuantity;
        }

        public void SubtractOneSnak()
        {
            if (_itemsQuantity > 0)
            {
                _itemsQuantity--;
            }
        }

        public void SetSnakId(int snakId)
        {
            if (_snakTypeId != 0 && _snakTypeId != snakId)
            {
                throw new IncorrectSlotDataException(nameof(snakId));
            }

            _snakTypeId = snakId;
        }
    }
}
