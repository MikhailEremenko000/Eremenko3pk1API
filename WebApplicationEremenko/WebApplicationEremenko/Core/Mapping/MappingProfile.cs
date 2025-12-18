using AutoMapper;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Models;

namespace WebApplicationEremenko.Core.Mapping
{
    /// <summary>
    /// Профиль маппинга AutoMapper для преобразования между сущностями и DTO
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Конструктор профиля маппинга с настройкой всех преобразований
        /// </summary>
        public MappingProfile()
        {
            /// <summary>
            /// Маппинг для мед. продуктов
            /// </summary>
            CreateMap<Pharmacy, PharmacyDto>().ReverseMap();
            CreateMap<CreatePharmacyDto, Pharmacy>();

            CreateMap<PharmacyProduct, PharmacyProductDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Pharmacy, PharmacyWithProductsDto>()
                .ForMember(dest => dest.Products,
                    opt => opt.MapFrom(src => src.PharmacyProducts));

            /// <summary>
            /// Маппинг для мед. продуктов
            /// </summary>
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();

            /// <summary>
            /// Маппинги для заказов
            /// </summary>
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderItems,
                    opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderItems,
                    opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<CreateOrderItemDto, OrderItem>();


            /// <summary>
            /// Маппинг для профиля клиента
            /// </summary>
            CreateMap<CustomerProfile, CustomerDto>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.User.PhoneNumber));

            /// <summary>
            /// Маппинг для создания клиента
            /// </summary>
            CreateMap<CreateCustomerDto, CustomerProfile>()
                .ForMember(dest => dest.User,
                    opt => opt.Ignore());

            /// <summary>
            /// Маппинг для обновления клиента
            /// </summary>
            CreateMap<UpdateCustomerDto, CustomerProfile>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            /// <summary>
            /// Маппинг для клиента с заказами
            /// </summary>
            CreateMap<CustomerProfile, CustomerWithOrdersDto>()
                .IncludeBase<CustomerProfile, CustomerDto>()
                .ForMember(dest => dest.Orders,
                    opt => opt.MapFrom(src => src.Orders));

            /// <summary>
            /// Маппинг для создания пользователя при регистрации клиента
            /// </summary>
            CreateMap<CreateCustomerDto, User>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(_ => "Customer"))
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.Ignore()); 
        }
    }
}
