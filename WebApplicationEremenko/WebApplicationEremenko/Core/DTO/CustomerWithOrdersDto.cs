namespace WebApplicationEremenko.Core.DTO
{
    /// DTO для представления клиента со списком его заказов
    /// </summary>
    public class CustomerWithOrdersDto : CustomerDto
    {
        /// <summary>
        /// Список заказов клиента
        /// </summary>
        public List<OrderDto> Orders { get; set; } = new();
    }
}
