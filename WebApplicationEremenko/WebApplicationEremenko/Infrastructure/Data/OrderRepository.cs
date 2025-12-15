using Microsoft.EntityFrameworkCore;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Data;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Infrastructure.Data
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerProfileId == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.CustomerProfile)
                .ThenInclude(c => c!.User)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByPharmacyAsync(int pharmacyId)
        {
            return await _context.Orders
                .Where(o => o.PharmacyId == pharmacyId)
                .ToListAsync();
        }
    }
}
