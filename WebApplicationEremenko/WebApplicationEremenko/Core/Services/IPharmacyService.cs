using WebApplicationEremenko.Core.DTO;

namespace WebApplicationEremenko.Core.Services
{
    public interface IPharmacyService
    {
        Task<IEnumerable<PharmacyDto>> GetAllPharmaciesAsync();

        Task<PharmacyWithProductsDto?> GetPharmacyWithProductsAsync(int id);

        Task<PharmacyDto> CreatePharmacyAsync(CreatePharmacyDto createPharmacyDto);

        Task<IEnumerable<PharmacyDto>> GetActivePharmaciesAsync();

        Task<bool> UpdatePharmacyAsync(int id, PharmacyDto pharmacyDto);

        Task<bool> DeletePharmacyAsync(int id);
    }

}
