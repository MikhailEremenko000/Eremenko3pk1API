using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;

        public string Status { get; set; } = "New";
        public DateTime? EstimatedDeliveryTime { get; set; }
        public string? PrescriptionImageUrl { get; set; }
        public int CustomerProfileId { get; set; }
        public CustomerProfile CustomerProfile { get; set; } = null!;

        public int? PharmacyId { get; set; }
        public Pharmacy? Pharmacy { get; set; }

        public int? CourierId { get; set; }
        public User? Courier { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
