using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    public class CreateOrderDto
    {
        [Required, MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        public int CustomerProfileId { get; set; }
        public int? PharmacyId { get; set; }

        [Required, MinLength(1)]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new();

        public string? PrescriptionImageUrl { get; set; }
    }
}
