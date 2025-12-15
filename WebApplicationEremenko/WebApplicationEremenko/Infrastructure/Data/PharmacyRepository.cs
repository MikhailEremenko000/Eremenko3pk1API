using Microsoft.EntityFrameworkCore;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Data;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Infrastructure.Data
{
    public class PharmacyRepository : Repository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<Pharmacy?> GetWithProductsAsync(int id)
        {
            return await _context.Pharmacies
                .Include(p => p.PharmacyProducts)
                .ThenInclude(pp => pp.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pharmacy>> GetActivePharmaciesAsync()
        {
            return await _context.Pharmacies
                .Where(p => p.IsActive)
                .ToListAsync();
        }
    }
}
