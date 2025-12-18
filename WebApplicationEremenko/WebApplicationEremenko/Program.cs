using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplicationEremenko.Core.Interfaces;
using WebApplicationEremenko.Core.Services;
using WebApplicationEremenko.Data;
using WebApplicationEremenko.Infrastructure.Data;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();

            //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}