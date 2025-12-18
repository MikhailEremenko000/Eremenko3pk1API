using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    /// <summary>
    /// DTO для создания информации о клиенте
    /// </summary>
    public class CreateCustomerDto
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Адрес доставки клиента
        /// </summary>
        [MaxLength(500)]
        public string? Address { get; set; }

        /// <summary>
        /// Номер медицинской карты
        /// </summary>
        [MaxLength(50)]
        public string? MedicalCardNumber { get; set; }

        /// <summary>
        /// email для регистрации
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Телефон для связи
        /// </summary>
        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Пароль для регистрации
        /// </summary>
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

}
