using AutoMapper;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMapper _mapper;

        public PharmacyService(IPharmacyRepository pharmacyRepository, IMapper mapper)
        {
            _pharmacyRepository = pharmacyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PharmacyDto>> GetAllPharmaciesAsync()
        {
            var pharmacies = await _pharmacyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PharmacyDto>>(pharmacies);
        }

        public async Task<PharmacyWithProductsDto?> GetPharmacyWithProductsAsync(int id)
        {
            var pharmacy = await _pharmacyRepository.GetWithProductsAsync(id);
            return pharmacy == null ? null : _mapper.Map<PharmacyWithProductsDto>(pharmacy);
        }

        public async Task<PharmacyDto> CreatePharmacyAsync(CreatePharmacyDto createPharmacyDto)
        {
            var pharmacy = _mapper.Map<Pharmacy>(createPharmacyDto);
            var createdPharmacy = await _pharmacyRepository.AddAsync(pharmacy);
            return _mapper.Map<PharmacyDto>(createdPharmacy);
        }

        public async Task<IEnumerable<PharmacyDto>> GetActivePharmaciesAsync()
        {
            var activePharmacies = await _pharmacyRepository.GetActivePharmaciesAsync();
            return _mapper.Map<IEnumerable<PharmacyDto>>(activePharmacies);
        }

        public async Task<bool> UpdatePharmacyAsync(int id, PharmacyDto pharmacyDto)
        {
            if (id != pharmacyDto.Id) return false;

            var pharmacy = await _pharmacyRepository.GetByIdAsync(id);
            if (pharmacy == null) return false;

            _mapper.Map(pharmacyDto, pharmacy);
            await _pharmacyRepository.UpdateAsync(pharmacy);
            return true;
        }

        public async Task<bool> DeletePharmacyAsync(int id)
        {
            var pharmacy = await _pharmacyRepository.GetByIdAsync(id);
            if (pharmacy == null) return false;

            pharmacy.IsActive = false;
            await _pharmacyRepository.UpdateAsync(pharmacy);
            return true;
        }
    }
}
