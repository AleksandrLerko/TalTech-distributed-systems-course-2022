using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Category, App.DAL.DTO.Category>().ReverseMap();
        CreateMap<App.BLL.DTO.CategoryType, App.DAL.DTO.CategoryType>().ReverseMap();
        CreateMap<App.BLL.DTO.Currency, App.DAL.DTO.Currency>().ReverseMap();
        CreateMap<App.BLL.DTO.Customer, App.DAL.DTO.Customer>().ReverseMap();
        CreateMap<App.BLL.DTO.DeliveryType, App.DAL.DTO.DeliveryType>().ReverseMap();
        CreateMap<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback>().ReverseMap();
        CreateMap<App.BLL.DTO.InStock, App.DAL.DTO.InStock>().ReverseMap();
        CreateMap<App.BLL.DTO.Location, App.DAL.DTO.Location>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, App.DAL.DTO.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.PaymentType, App.DAL.DTO.PaymentType>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, App.DAL.DTO.Product>().ReverseMap();
        CreateMap<App.BLL.DTO.Seller, App.DAL.DTO.Seller>().ReverseMap();
        CreateMap<App.BLL.DTO.ShippingInfoAppUser, App.DAL.DTO.ShippingInfoAppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.ShippingInfoCustomer, App.DAL.DTO.ShippingInfoCustomer>().ReverseMap();
        CreateMap<App.BLL.DTO.ShippingInfo, App.DAL.DTO.ShippingInfo>().ReverseMap();
        CreateMap<App.BLL.DTO.Specification, App.DAL.DTO.Specification>().ReverseMap();
        CreateMap<App.BLL.DTO.SpecificationType, App.DAL.DTO.SpecificationType>().ReverseMap();
        CreateMap<App.BLL.DTO.TransactionReport, App.DAL.DTO.TransactionReport>().ReverseMap();
        CreateMap<App.BLL.DTO.Picture, App.DAL.DTO.Picture>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductOrders, App.DAL.DTO.ProductOrders>().ReverseMap();
        CreateMap<App.BLL.DTO.Invoice, App.DAL.DTO.Invoice>().ReverseMap();
    }
}