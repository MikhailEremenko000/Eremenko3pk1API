using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Services;

/// <summary>
/// Сервис для управления клиентами и их профилями
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    /// <summary>
    /// Конструктор сервиса клиентов
    /// </summary>
    public CustomerService(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IRepository<User> userRepository,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    /// <summary>
    /// Получить список клиентов с краткой информацией
    /// </summary>
    public async Task<IEnumerable<CustomerShortInfoDto>> GetAllCustomersShortInfoAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerShortInfoDto>>(customers);
    }

    /// <summary>
    /// Получить клиента по его идентификатору
    /// </summary>
    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }

    /// <summary>
    /// Получить клиента по идентификатору пользователя
    /// </summary>
    public async Task<CustomerDto?> GetCustomerByUserIdAsync(int userId)
    {
        var customer = await _customerRepository.GetByUserIdAsync(userId);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }

    /// <summary>
    /// Получить клиента с полной информацией о его заказах
    /// </summary>
    public async Task<CustomerWithOrdersDto?> GetCustomerWithOrdersAsync(int id)
    {
        var customer = await _customerRepository.GetWithOrdersAsync(id);
        if (customer == null) return null;

        var dto = _mapper.Map<CustomerWithOrdersDto>(customer);
        return dto;
    }

    /// <summary>
    /// Создать нового клиента с регистрацией пользователя
    /// </summary>
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var existingUser = (await _userRepository.FindAsync(u => u.Email == createCustomerDto.Email))
            .FirstOrDefault();

        if (existingUser != null)
        {
            throw new ArgumentException($"Пользователь с email '{createCustomerDto.Email}' уже существует");
        }

        var user = _mapper.Map<User>(createCustomerDto);
        user.PasswordHash = _passwordHasher.HashPassword(user, createCustomerDto.Password);
        user.CreatedAt = DateTime.UtcNow;

        var createdUser = await _userRepository.AddAsync(user);

        var customerProfile = _mapper.Map<CustomerProfile>(createCustomerDto);
        customerProfile.UserId = createdUser.Id;

        var createdCustomer = await _customerRepository.AddAsync(customerProfile);
        return _mapper.Map<CustomerDto>(createdCustomer);
    }

    /// <summary>
    /// Обновить информацию о существующем клиенте
    /// </summary>
    public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return false;

        _mapper.Map(updateCustomerDto, customer);

        if (!string.IsNullOrEmpty(updateCustomerDto.PhoneNumber))
        {
            var user = await _userRepository.GetByIdAsync(customer.UserId);
            if (user != null)
            {
                user.PhoneNumber = updateCustomerDto.PhoneNumber;
                await _userRepository.UpdateAsync(user);
            }
        }

        await _customerRepository.UpdateAsync(customer);
        return true;
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return false;

        var user = await _userRepository.GetByIdAsync(customer.UserId);
        if (user != null)
        {
            user.Email = $"deleted_{DateTime.Now.Ticks}_{user.Email}";
            user.PhoneNumber = null;
            await _userRepository.UpdateAsync(user);
        }

        await _customerRepository.DeleteAsync(customer);
        return true;
    }

    /// <summary>
    /// Проверить, требуется ли клиенту медицинская карта для заказа
    /// </summary>
    public async Task<bool> HasValidMedicalCardAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null) return false;

        return !string.IsNullOrEmpty(customer.MedicalCardNumber);
    }

    /// <summary>
    /// Обновить номер медицинской карты клиента
    /// </summary>
    public async Task<bool> UpdateMedicalCardAsync(int customerId, string medicalCardNumber)
    {
        if (string.IsNullOrWhiteSpace(medicalCardNumber))
            throw new ArgumentException("Номер медицинской карты не может быть пустым");

        if (medicalCardNumber.Length < 5 || medicalCardNumber.Length > 50)
            throw new ArgumentException("Номер медицинской карты должен быть от 5 до 50 символов");

        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null) return false;

        customer.MedicalCardNumber = medicalCardNumber;
        await _customerRepository.UpdateAsync(customer);
        return true;
    }

    /// <summary>
    /// Получить статистику по клиенту
    /// </summary>
    public async Task<CustomerStatsDto> GetCustomerStatsAsync(int customerId)
    {
        var customer = await _customerRepository.GetWithOrdersAsync(customerId);
        if (customer == null)
            throw new ArgumentException($"Клиент с ID {customerId} не найден");

        var orders = customer.Orders;

        var stats = new CustomerStatsDto
        {
            TotalOrders = orders.Count,
            CompletedOrders = orders.Count(o => o.Status == "Delivered"),
            ActiveOrders = orders.Count(o => o.Status != "Delivered" && o.Status != "Cancelled"),
            TotalSpent = orders.Where(o => o.Status == "Delivered").Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPriceAtOrderTime)),
            FirstOrderDate = orders.Min(o => (DateTime?)o.CreatedAt),
            LastOrderDate = orders.Max(o => (DateTime?)o.CreatedAt),
            PrescriptionOrders = orders.Count(o => o.OrderItems.Any(oi => oi.Product.IsPrescriptionRequired))
        };

        if (stats.CompletedOrders > 0)
        {
            stats.AverageOrderValue = stats.TotalSpent / stats.CompletedOrders;
        }

        return stats;
    }

    /// <summary>
    /// Поиск клиентов по имени или email
    /// </summary>
    public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<CustomerDto>();

        var customers = await _customerRepository.FindAsync(c =>
            c.FirstName.Contains(searchTerm) ||
            c.LastName.Contains(searchTerm) ||
            c.User.Email.Contains(searchTerm));

        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    /// <summary>
    /// Проверить, может ли клиент заказывать рецептурные препараты
    /// </summary>
    public async Task<bool> CanOrderPrescriptionDrugsAsync(int customerId)
    {
        return await HasValidMedicalCardAsync(customerId);
    }

    /// <summary>
    /// Получить адрес доставки клиента по умолчанию
    /// </summary>
    public async Task<string?> GetDefaultDeliveryAddressAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        return customer?.Address;
    }

    /// <summary>
    /// Получить историю адресов доставки клиента
    /// </summary>s
    public async Task<IEnumerable<string>> GetDeliveryAddressHistoryAsync(int customerId)
    {
        var orders = await _orderRepository.GetOrdersByCustomerAsync(customerId);
        return orders
            .Select(o => o.DeliveryAddress)
            .Where(addr => !string.IsNullOrEmpty(addr))
            .Distinct()
            .ToList();
    }
}
