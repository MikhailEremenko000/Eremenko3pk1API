using System.ComponentModel.DataAnnotations;

namespace WebApplicationEremenko.Core.DTO
{
    /// <summary>
    /// DTO для представления информации о клиенте
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Уникальный идентификатор клиента
        /// </summary>
        public int Id { get; set; }

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
        /// Идентификатор связанного пользователя
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// email клиента
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Телефон клиента (из связанной сущности User)
        /// </summary>
        public string? PhoneNumber { get; set; }
    }

}