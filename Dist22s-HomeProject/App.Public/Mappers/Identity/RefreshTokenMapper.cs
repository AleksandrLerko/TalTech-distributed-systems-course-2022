using App.Public.DTO.v1.Identity;

namespace App.Public.Mappers.Identity;

public class RefreshTokenMapper
{
    // public static App.BLL.DTO.Identity.RefreshToken MapToBll(RefreshToken refreshToken)
    // {
    //     return new App.BLL.DTO.Identity.RefreshToken()
    //     {
    //         Token = refreshToken.Token,
    //         TokenExpirationDateTime = refreshToken.TokenExpirationDateTime,
    //         PreviousToken = refreshToken.PreviousToken,
    //         PreviousTokenExpirationDateTime = refreshToken.PreviousTokenExpirationDateTime,
    //         AppUserId = refreshToken.AppUserId,
    //         // AppUser = refreshToken.AppUser != null ? AppUserMapper.MapToBll(refreshToken.AppUser) : null
    //     };
    // }
    //
    // public static RefreshToken MapFromBll(App.BLL.DTO.Identity.RefreshToken refreshToken)
    // {
    //     return new RefreshToken()
    //     {
    //         Token = refreshToken.Token,
    //         TokenExpirationDateTime = refreshToken.TokenExpirationDateTime,
    //         PreviousToken = refreshToken.PreviousToken,
    //         PreviousTokenExpirationDateTime = refreshToken.PreviousTokenExpirationDateTime,
    //         AppUserId = refreshToken.AppUserId,
    //         // AppUser = refreshToken.AppUser != null ? AppUserMapper.MapFromBll(refreshToken.AppUser) : null
    //     };
    // }
}