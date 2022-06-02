using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Order = App.Public.DTO.v1.Order;

namespace App.Public.Mappers;

public class OrderMapper : BaseMapper<Order ,App.BLL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Order MapToBll(Order order)
    {
        return new BLL.DTO.Order()
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerId = order.CustomerId,
            // Customer = order.Customer != null ? CustomerMapper.MapToBll(order.Customer) : null,
            AppUserId = order.AppUserId,
            DeliveryTypeId = order.DeliveryTypeId,
            ShippingInfoId = order.ShippingInfoId,
            // DeliveryType = order.DeliveryType != null ? DeliveryTypeMapper.MapToBll(order.DeliveryType) : null,
            PaymentTypeId = order.PaymentTypeId,
            // Product = order.Product != null ? ProductMapper.MapToBll(order.Product) : null,
            TransactionReport = order.TransactionReport != null ? TransactionReportMapper.MapToBll(order.TransactionReport): null,
        };
    }
    
    public static Order MapFromBll(BLL.DTO.Order order)
    {
        return new Order()
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerId = order.CustomerId,
            // Customer = order.Customer != null ? CustomerMapper.MapFromBll(order.Customer) : null,
            DeliveryTypeId = order.DeliveryTypeId,
            ShippingInfoId = order.ShippingInfoId,
            // DeliveryType = order.DeliveryType != null ? DeliveryTypeMapper.MapFromBll(order.DeliveryType) : null,
            PaymentTypeId = order.PaymentTypeId,
            // Product = order.Product != null ? ProductMapper.MapFromBll(order.Product) : null,
            TransactionReport = order.TransactionReport != null ? TransactionReportMapper.MapFromBll(order.TransactionReport): null,
        };
    }
}