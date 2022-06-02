using App.Public.DTO.v1;
using App.Public.Mappers.Identity;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ShippingInfoAppUserMapper : BaseMapper<ShippingInfoAppUser ,App.BLL.DTO.ShippingInfoAppUser>
{
    public ShippingInfoAppUserMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.ShippingInfoAppUser MapToBll(ShippingInfoAppUser shippingInfoAppUser)
    {
        return new BLL.DTO.ShippingInfoAppUser()
        {
            Id = shippingInfoAppUser.Id,
            AppUserId = shippingInfoAppUser.AppUserId,
            // AppUser = shippingInfoAppUser.AppUser != null ? AppUserMapper.MapToBll(shippingInfoAppUser.AppUser) : null,
            ShippingInfoId = shippingInfoAppUser.ShippingInfoId,
            // ShippingInfo = shippingInfoAppUser.ShippingInfo != null ? ShippingInfoMapper.MapToBll(shippingInfoAppUser.ShippingInfo) : null
        };
    }
    
    public static ShippingInfoAppUser MapFromBll(BLL.DTO.ShippingInfoAppUser shippingInfoAppUser)
    {
        return new ShippingInfoAppUser()
        {
            Id = shippingInfoAppUser.Id,
            AppUserId = shippingInfoAppUser.AppUserId,
            // AppUser = shippingInfoAppUser.AppUser != null ? AppUserMapper.MapFromBll(shippingInfoAppUser.AppUser) : null,
            ShippingInfoId = shippingInfoAppUser.ShippingInfoId,
            // ShippingInfo = shippingInfoAppUser.ShippingInfo != null ? ShippingInfoMapper.MapFromBll(shippingInfoAppUser.ShippingInfo) : null
        };
    }
}