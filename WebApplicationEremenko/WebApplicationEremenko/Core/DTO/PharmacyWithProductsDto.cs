namespace WebApplicationEremenko.Core.DTO
{
    public class PharmacyWithProductsDto : PharmacyDto
    {
        public List<PharmacyProductDto> Products { get; set; } = new();
    }
}
