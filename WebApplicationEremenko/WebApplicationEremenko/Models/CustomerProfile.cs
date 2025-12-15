namespace WebApplicationEremenko.Models
{
    public class CustomerProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? MedicalCardNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
