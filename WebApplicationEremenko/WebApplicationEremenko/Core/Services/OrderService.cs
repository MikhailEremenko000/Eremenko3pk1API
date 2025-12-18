using AutoMapper;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IPharmacyRepository pharmacyRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _pharmacyRepository = pharmacyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            foreach (var item in createOrderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Товар с ID {item.ProductId} не найден");

                if (product.IsPrescriptionRequired && string.IsNullOrEmpty(createOrderDto.PrescriptionImageUrl))
                    throw new InvalidOperationException($"Для товара '{product.Name}' требуется рецепт");
            }

            var order = _mapper.Map<Order>(createOrderDto);
            order.CreatedAt = DateTime.UtcNow;

            order.OrderItems = new List<OrderItem>();
            foreach (var itemDto in createOrderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPriceAtOrderTime = product!.BasePrice
                });
            }

            var createdOrder = await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateDto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return false;

            var allowedTransitions = new Dictionary<string, List<string>>
            {
                ["New"] = new List<string> { "Processing", "Cancelled" },
                ["Processing"] = new List<string> { "AwaitingPickup", "Cancelled" },
                ["AwaitingPickup"] = new List<string> { "OnTheWay" },
                ["OnTheWay"] = new List<string> { "Delivered", "Cancelled" },
                ["Delivered"] = new List<string>(),
                ["Cancelled"] = new List<string>()
            };

            if (allowedTransitions.ContainsKey(order.Status) &&
                !allowedTransitions[order.Status].Contains(updateDto.Status))
            {
                throw new InvalidOperationException(
                    $"Недопустимый переход статуса из '{order.Status}' в '{updateDto.Status}'");
            }

            order.Status = updateDto.Status;
            order.EstimatedDeliveryTime = updateDto.EstimatedDeliveryTime;
            order.CourierId = updateDto.CourierId;
            order.PharmacyId = updateDto.PharmacyId;

            await _orderRepository.UpdateAsync(order);
            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
