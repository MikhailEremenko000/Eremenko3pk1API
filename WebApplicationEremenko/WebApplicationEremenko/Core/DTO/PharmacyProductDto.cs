namespace WebApplicationEremenko.Core.DTO
{
    public class PharmacyProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal ActualPrice { get; set; }
    }
}
