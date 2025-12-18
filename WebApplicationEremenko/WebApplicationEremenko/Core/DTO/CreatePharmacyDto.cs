using System.ComponentModel.DataAnnotations;
namespace WebApplicationEremenko.Core.DTO
{
    public class CreatePharmacyDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required, Phone]
        public string ContactPhone { get; set; } = string.Empty;
    }
}
