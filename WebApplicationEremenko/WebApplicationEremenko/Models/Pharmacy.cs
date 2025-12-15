namespace WebApplicationEremenko.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<PharmacyProduct> PharmacyProducts { get; set; } = new();
    }
}
