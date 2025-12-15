namespace WebApplicationEremenko.Models
{
    public class PharmacyProduct
    {
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int StockQuantity { get; set; }
        public decimal ActualPrice { get; set; }
    }
}
