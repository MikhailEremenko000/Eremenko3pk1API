using Microsoft.EntityFrameworkCore;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Data;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Infrastructure.Data
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPrescriptionRequiredProductsAsync(bool requiresPrescription)
        {
            return await _context.Products
                .Where(p => p.IsPrescriptionRequired == requiresPrescription)
                .ToListAsync();
        }

        public async Task<Product?> GetWithPharmaciesAsync(int id)
        {
            return await _context.Products
                .Include(p => p.PharmacyProducts)
                .ThenInclude(pp => pp.Pharmacy)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
