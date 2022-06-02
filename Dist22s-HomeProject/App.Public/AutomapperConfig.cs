using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;

namespace App.Public;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Category, App.BLL.DTO.Category>().ReverseMap();
        CreateMap<CategoryType, App.BLL.DTO.CategoryType>().ReverseMap();
        CreateMap<Currency, App.BLL.DTO.Currency>().ReverseMap();
        CreateMap<Customer, App.BLL.DTO.Customer>().ReverseMap();
        CreateMap<DeliveryType, App.BLL.DTO.DeliveryType>().ReverseMap();
        CreateMap<Feedback, App.BLL.DTO.Feedback>().ReverseMap();
        CreateMap<InStock, App.BLL.DTO.InStock>().ReverseMap();
        CreateMap<Location, App.BLL.DTO.Location>().ReverseMap();
        CreateMap<Order, App.BLL.DTO.Order>().ReverseMap();
        CreateMap<PaymentType, App.BLL.DTO.PaymentType>().ReverseMap();
        CreateMap<Product, App.BLL.DTO.Product>().ReverseMap();
        CreateMap<Seller, App.BLL.DTO.Seller>().ReverseMap();
        CreateMap<ShippingInfoAppUser, App.BLL.DTO.ShippingInfoAppUser>().ReverseMap();
        CreateMap<ShippingInfo, App.BLL.DTO.ShippingInfo>().ReverseMap();
        CreateMap<Specification, App.BLL.DTO.Specification>().ReverseMap();
        CreateMap<SpecificationType, App.BLL.DTO.SpecificationType>().ReverseMap();
        CreateMap<TransactionReport, App.BLL.DTO.TransactionReport>().ReverseMap();
        CreateMap<Picture, App.BLL.DTO.Picture>().ReverseMap();
        CreateMap<AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<ProductOrders, App.BLL.DTO.ProductOrders>().ReverseMap();
    }
}