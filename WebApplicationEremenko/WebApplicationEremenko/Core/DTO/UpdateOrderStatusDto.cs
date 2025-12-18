using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime? EstimatedDeliveryTime { get; set; }
        public int? CourierId { get; set; }
        public int? PharmacyId { get; set; }
    }

}
