namespace WebApplicationEremenko.Core.DTO
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPriceAtOrderTime { get; set; }
        public decimal TotalPrice => Quantity * UnitPriceAtOrderTime;
    }
}
