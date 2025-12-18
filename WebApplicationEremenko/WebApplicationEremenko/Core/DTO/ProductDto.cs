using System.ComponentModel.DataAnnotations;
namespace WebApplicationEremenko.Core.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required, MaxLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 10000)]
        public decimal BasePrice { get; set; }

        public bool IsPrescriptionRequired { get; set; } = false;

        public string? Category { get; set; }
    }
}
