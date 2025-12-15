using WebApplicationEremenko.Models;
namespace WebApplicationEremenko.Core.Interfaces
{
    public interface IPharmacyRepository : IRepository<Pharmacy>
    {
        Task<Pharmacy?> GetWithProductsAsync(int id);
        Task<IEnumerable<Pharmacy>> GetActivePharmaciesAsync();
    }
}
