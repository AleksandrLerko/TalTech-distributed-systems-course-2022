using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using ShippingInfoAppUser = App.BLL.DTO.ShippingInfoAppUser;
using ShippingInfoCustomer = App.BLL.DTO.ShippingInfoCustomer;

namespace App.Public.Mappers;

public class ShippingInfoMapper : BaseMapper<ShippingInfo ,App.BLL.DTO.ShippingInfo>
{
    public ShippingInfoMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.ShippingInfo MapToBll(ShippingInfo shippingInfo)
    {
        return new BLL.DTO.ShippingInfo()
        {
            Id = shippingInfo.Id,
            AddressOne = shippingInfo.AddressOne,
            AddressTwo = shippingInfo.AddressTwo,
            // DeliveryType = shippingInfo.DeliveryType != null ? DeliveryTypeMapper.MapToBll(shippingInfo.DeliveryType) : null,
            ShippingInfoAppUsers = shippingInfo.ShippingInfoAppUsers != null ? shippingInfo.ShippingInfoAppUsers.Select(x => ShippingInfoAppUserMapper.MapToBll(x)).ToList() : new List<ShippingInfoAppUser>(),
            Customer = shippingInfo.Customer != null ? CustomerMapper.MapToBll(shippingInfo.Customer): null,
            Orders = shippingInfo.Orders != null ? shippingInfo.Orders.Select(x => OrderMapper.MapToBll(x)).ToList() : new List<App.BLL.DTO.Order>()
        };
    }
    
    public static ShippingInfo MapFromBll(BLL.DTO.ShippingInfo shippingInfo)
    {
        return new ShippingInfo()
        {
            Id = shippingInfo.Id,
            AddressOne = shippingInfo.AddressOne,
            AddressTwo = shippingInfo.AddressTwo,
            // DeliveryType = shippingInfo.DeliveryType != null ? DeliveryTypeMapper.MapFromBll(shippingInfo.DeliveryType) : null,
            ShippingInfoAppUsers = shippingInfo.ShippingInfoAppUsers != null ? shippingInfo.ShippingInfoAppUsers.Select(x => ShippingInfoAppUserMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.ShippingInfoAppUser>(),
            Customer = shippingInfo.Customer != null ? CustomerMapper.MapFromBll(shippingInfo.Customer): null,
            Orders = shippingInfo.Orders != null ? shippingInfo.Orders.Select(x => OrderMapper.MapFromBll(x)).ToList() : new List<Order>()
        };
    }
}