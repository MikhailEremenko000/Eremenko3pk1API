
namespace WebApplicationEremenko.Models
{
    public class User
    {
        internal DateTime CreatedAt;

        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer";
        public CustomerProfile? CustomerProfile { get; set; }
    }
}
