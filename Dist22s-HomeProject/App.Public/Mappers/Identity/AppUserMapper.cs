using App.BLL.DTO;
using App.Public.DTO.v1.Identity;
using AutoMapper;
using Base.DAL;
using RefreshToken = App.BLL.DTO.Identity.RefreshToken;
using RefreshTokenModel = App.BLL.DTO.Identity.RefreshTokenModel;

namespace App.Public.Mappers.Identity;

public class AppUserMapper : BaseMapper<AppUser ,App.BLL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Identity.AppUser MapToBll(AppUser appUser)
    {
        return new BLL.DTO.Identity.AppUser()
        {
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            RefreshTokens =  new List<RefreshToken>(),
            ShippingInfoAppUsers =  new List<ShippingInfoAppUser>(),
            Feedbacks =  new List<Feedback>(),
            Orders =  new List<Order>()
        };
    }
    
    public static AppUser MapFromBll(BLL.DTO.Identity.AppUser appUser)
    {
        return new AppUser()
        {
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            RefreshTokens = new List<App.Public.DTO.v1.Identity.RefreshToken>(),
            ShippingInfoAppUsers =  new List<App.Public.DTO.v1.ShippingInfoAppUser>(),
            Feedbacks =  new List<App.Public.DTO.v1.Feedback>(),
            Orders =  new List<App.Public.DTO.v1.Order>()
        };
    }
}