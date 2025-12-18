using AutoMapper;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Models;
namespace WebApplicationEremenko.Core.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        Task<OrderDto?> GetOrderByIdAsync(int id);

        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);

        Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateDto);

        Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId);

        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
    }
}
