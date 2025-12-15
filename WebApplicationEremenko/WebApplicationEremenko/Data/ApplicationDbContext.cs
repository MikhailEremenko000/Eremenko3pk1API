using Microsoft.EntityFrameworkCore;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PharmacyProduct> PharmacyProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerProfile>()
                .HasOne(cp => cp.User)
                .WithOne(u => u.CustomerProfile)
                .HasForeignKey<CustomerProfile>(cp => cp.UserId);

            modelBuilder.Entity<PharmacyProduct>()
                .HasKey(pp => new { pp.PharmacyId, pp.ProductId });

            modelBuilder.Entity<PharmacyProduct>()
                .HasOne(pp => pp.Pharmacy)
                .WithMany(p => p.PharmacyProducts)
                .HasForeignKey(pp => pp.PharmacyId);

            modelBuilder.Entity<PharmacyProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.PharmacyProducts)
                .HasForeignKey(pp => pp.ProductId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Pharmacy)
                .WithMany()
                .HasForeignKey(o => o.PharmacyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Courier)
                .WithMany()
                .HasForeignKey(o => o.CourierId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
