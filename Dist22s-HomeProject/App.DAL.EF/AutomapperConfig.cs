using App.Domain;
using App.Domain.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.DAL.DTO.Category, Category>().ReverseMap();
        CreateMap<App.DAL.DTO.CategoryType, CategoryType>().ReverseMap();
        CreateMap<App.DAL.DTO.Currency, Currency>().ReverseMap();
        CreateMap<App.DAL.DTO.Customer, Customer>().ReverseMap();
        CreateMap<App.DAL.DTO.DeliveryType, DeliveryType>().ReverseMap();
        CreateMap<App.DAL.DTO.Feedback, Feedback>().ReverseMap();
        CreateMap<App.DAL.DTO.InStock, InStock>().ReverseMap();
        CreateMap<App.DAL.DTO.Location, Location>().ReverseMap();
        CreateMap<App.DAL.DTO.Order, Order>().ReverseMap();
        CreateMap<App.DAL.DTO.PaymentType, PaymentType>().ReverseMap();
        CreateMap<App.DAL.DTO.Product, Product>().ReverseMap();
        CreateMap<App.DAL.DTO.Seller, Seller>().ReverseMap();
        CreateMap<App.DAL.DTO.ShippingInfoAppUser, ShippingInfoAppUser>().ReverseMap();
        CreateMap<App.DAL.DTO.ShippingInfoCustomer, ShippingInfoCustomer>().ReverseMap();
        CreateMap<App.DAL.DTO.ShippingInfo, ShippingInfo>().ReverseMap();
        CreateMap<App.DAL.DTO.Specification, Specification>().ReverseMap();
        CreateMap<App.DAL.DTO.SpecificationType, SpecificationType>().ReverseMap();
        CreateMap<App.DAL.DTO.TransactionReport, TransactionReport>().ReverseMap();
        CreateMap<App.DAL.DTO.Picture, Picture>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, AppUser>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.RefreshToken, RefreshToken>().ReverseMap();
        CreateMap<App.DAL.DTO.ProductOrders, ProductOrders>().ReverseMap();
        CreateMap<App.DAL.DTO.Invoice, Invoice>().ReverseMap();
    }
}