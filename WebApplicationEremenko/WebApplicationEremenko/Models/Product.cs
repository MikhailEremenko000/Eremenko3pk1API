using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public decimal BasePrice { get; set; }

        public bool IsPrescriptionRequired { get; set; } = false; 

        public List<PharmacyProduct> PharmacyProducts { get; set; } = new(); 
        public List<OrderItem> OrderItems { get; set; } = new(); 
    }
}
