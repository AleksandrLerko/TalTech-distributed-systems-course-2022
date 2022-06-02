using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using DeliveryType = App.Public.DTO.v1.DeliveryType;

namespace App.Public.Mappers;

public class DeliveryTypeMapper : BaseMapper<DeliveryType ,App.BLL.DTO.DeliveryType>
{
    public DeliveryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.DeliveryType MapToBll(DeliveryType deliveryType)
    {
        return new BLL.DTO.DeliveryType()
        {
            Id = deliveryType.Id,
            TypeName = deliveryType.TypeName,
            Comment = deliveryType.Comment,
            Price = deliveryType.Price,
            ShippingInfos = deliveryType.ShippingInfos != null ? deliveryType.ShippingInfos.Select(x => ShippingInfoMapper.MapToBll(x)).ToList() : new List<ShippingInfo>(),
            Orders = deliveryType.Orders != null ? deliveryType.Orders.Select(x => OrderMapper.MapToBll(x)).ToList() : new List<Order>()
        };
    }
    
    public static DeliveryType MapFromBll(BLL.DTO.DeliveryType deliveryType)
    {
        return new DeliveryType()
        {
            Id = deliveryType.Id,
            TypeName = deliveryType.TypeName,
            Comment = deliveryType.Comment,
            Price = deliveryType.Price,
            ShippingInfos = deliveryType.ShippingInfos != null ? deliveryType.ShippingInfos.Select(x => ShippingInfoMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.ShippingInfo>(),
            Orders = deliveryType.Orders != null ? deliveryType.Orders.Select(x => OrderMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Order>()
        };
    }
}