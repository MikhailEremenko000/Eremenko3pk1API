using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required, MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        public string Status { get; set; } = "New";
        public DateTime? EstimatedDeliveryTime { get; set; }

        public int CustomerProfileId { get; set; }
        public int? PharmacyId { get; set; }
        public int? CourierId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
