using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<CustomerProfile>
    {
        Task<CustomerProfile?> GetByUserIdAsync(int userId);
        Task<CustomerProfile?> GetWithOrdersAsync(int id);
    }
}
