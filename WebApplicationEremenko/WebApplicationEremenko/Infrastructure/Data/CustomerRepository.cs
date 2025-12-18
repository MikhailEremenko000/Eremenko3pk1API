using Microsoft.EntityFrameworkCore;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Data;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Infrastructure.Data
{
    public class CustomerRepository : Repository<CustomerProfile>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CustomerProfile?> GetByUserIdAsync(int userId)
        {
            return await _context.CustomerProfiles
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<CustomerProfile?> GetWithOrdersAsync(int id)
        {
            return await _context.CustomerProfiles
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}