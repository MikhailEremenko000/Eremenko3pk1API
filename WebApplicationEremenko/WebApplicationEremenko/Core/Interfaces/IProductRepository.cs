using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task<IEnumerable<Product>> GetPrescriptionRequiredProductsAsync(bool requiresPrescription);
        Task<Product?> GetWithPharmaciesAsync(int id);
    }
}
