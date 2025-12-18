using Microsoft.AspNetCore.Mvc;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmaciesController : ControllerBase
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public PharmaciesController(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pharmacy>>> GetPharmacies()
        {
            var pharmacies = await _pharmacyRepository.GetAllAsync();
            return Ok(pharmacies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pharmacy>> GetPharmacy(int id)
        {
            var pharmacy = await _pharmacyRepository.GetByIdAsync(id);

            if (pharmacy == null)
                return NotFound();

            return Ok(pharmacy);
        }

        [HttpPost]
        public async Task<ActionResult<Pharmacy>> CreatePharmacy(Pharmacy pharmacy)
        {
            var createdPharmacy = await _pharmacyRepository.AddAsync(pharmacy);
            return CreatedAtAction(nameof(GetPharmacy), new { id = createdPharmacy.Id }, createdPharmacy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePharmacy(int id, Pharmacy pharmacy)
        {
            if (id != pharmacy.Id)
                return BadRequest();

            await _pharmacyRepository.UpdateAsync(pharmacy);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacy(int id)
        {
            var pharmacy = await _pharmacyRepository.GetByIdAsync(id);

            if (pharmacy == null)
                return NotFound();

            await _pharmacyRepository.DeleteAsync(pharmacy);
            return NoContent();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Pharmacy>>> GetActivePharmacies()
        {
            var activePharmacies = await _pharmacyRepository.GetActivePharmaciesAsync();
            return Ok(activePharmacies);
        }
    }
}
