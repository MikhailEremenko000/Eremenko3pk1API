using WebApplicationEremenko.Core.DTO;

namespace WebApplicationEremenko.Core.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Получить список всех клиентов
        /// </summary>
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Получить список клиентов с краткой информацией
        /// </summary>
        Task<IEnumerable<CustomerShortInfoDto>> GetAllCustomersShortInfoAsync();

        /// <summary>
        /// Получить клиента по его идентификатору
        /// </summary>
        Task<CustomerDto?> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Получить клиента по идентификатору пользователя
        /// </summary>
        Task<CustomerDto?> GetCustomerByUserIdAsync(int userId);

        /// <summary>
        /// Получить клиента с полной информацией о его заказах
        /// </summary>
        Task<CustomerWithOrdersDto?> GetCustomerWithOrdersAsync(int id);

        /// <summary>
        /// Создать нового клиента с регистрацией пользователя
        /// </summary>
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);

        /// <summary>
        /// Обновить информацию о существующем клиенте
        /// </summary>
        Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        Task<bool> DeleteCustomerAsync(int id);

        /// <summary>
        /// Проверить, требуется ли клиенту медицинская карта для заказа
        /// </summary>
        Task<bool> HasValidMedicalCardAsync(int customerId);

        /// <summary>
        /// Обновить номер медицинской карты клиента
        /// </summary>
        Task<bool> UpdateMedicalCardAsync(int customerId, string medicalCardNumber);

        /// <summary>
        /// Получить статистику по клиенту
        /// </summary>
        Task<CustomerStatsDto> GetCustomerStatsAsync(int customerId);

        /// <summary>
        /// Поиск клиентов по имени или email
        /// </summary>
        Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm);

        /// <summary>
        /// Проверить, может ли клиент заказывать рецептурные препараты
        /// </summary>
        Task<bool> CanOrderPrescriptionDrugsAsync(int customerId);

        /// <summary>
        /// Получить адрес доставки клиента по умолчанию
        /// </summary>
        Task<string?> GetDefaultDeliveryAddressAsync(int customerId);

        /// <summary>
        /// Получить историю адресов доставки клиента
        /// </summary>
        Task<IEnumerable<string>> GetDeliveryAddressHistoryAsync(int customerId);
    }

}
