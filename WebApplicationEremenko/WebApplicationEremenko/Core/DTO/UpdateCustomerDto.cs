using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    /// <summary>
    /// DTO для обновления информации о клиенте
    /// </summary>
    public class UpdateCustomerDto
    {
        /// <summary>
        /// Новое имя клиента (опционально)
        /// </summary>
        [MaxLength(100)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Новая фамилия клиента (опционально)
        /// </summary>
        [MaxLength(100)]
        public string? LastName { get; set; }

        /// <summary>
        /// Новый адрес доставки (опционально)
        /// </summary>
        [MaxLength(500)]
        public string? Address { get; set; }

        /// <summary>
        /// Новый номер медицинской карты (опционально)
        /// </summary>
        [MaxLength(50)]
        public string? MedicalCardNumber { get; set; }

        /// <summary>
        /// Новый телефон для связи (опционально)
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }
    }


}
