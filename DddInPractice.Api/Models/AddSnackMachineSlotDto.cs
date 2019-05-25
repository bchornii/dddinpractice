namespace DddInPractice.Api.Models
{
    public class AddSnackMachineSlotDto
    {
        public int Position { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int SnackTypeId { get; set; }
    }
}
