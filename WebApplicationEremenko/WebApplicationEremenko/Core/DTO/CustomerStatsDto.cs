namespace WebApplicationEremenko.Core.DTO
{
    public class CustomerStatsDto
    {
        /// <summary>
        /// Общее количество заказов
        /// </summary>
        public int TotalOrders { get; set; }

        /// <summary>
        /// Количество завершенных заказов
        /// </summary>
        public int CompletedOrders { get; set; }

        /// <summary>
        /// Количество активных заказов
        /// </summary>
        public int ActiveOrders { get; set; }

        /// <summary>
        /// Общая сумма всех заказов
        /// </summary>
        public decimal TotalSpent { get; set; }

        /// <summary>
        /// Средняя стоимость заказа
        /// </summary>
        public decimal AverageOrderValue { get; set; }

        /// <summary>
        /// Дата первого заказа
        /// </summary>
        public DateTime? FirstOrderDate { get; set; }

        /// <summary>
        /// Дата последнего заказа
        /// </summary>
        public DateTime? LastOrderDate { get; set; }

        /// <summary>
        /// Количество рецептурных заказов
        /// </summary>
        public int PrescriptionOrders { get; set; }

        /// <summary>
        /// Самый часто заказываемый продукт
        /// </summary>
        public string? MostOrderedProduct { get; set; }

        /// <summary>
        /// Чаще всего заказывает
        /// </summary>
        public string? FavoritePharmacy { get; set; }
    }
}
