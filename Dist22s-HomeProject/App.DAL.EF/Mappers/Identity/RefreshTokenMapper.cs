using App.Domain.Identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class RefreshTokenMapper : BaseMapper<App.DAL.DTO.Identity.RefreshToken, RefreshToken>
{
    // public static RefreshToken MapFromDAL(App.DAL.DTO.Identity.RefreshToken refreshToken)
    // {
    //     return new RefreshToken()
    //     {
    //         Token = refreshToken.Token,
    //         TokenExpirationDateTime = refreshToken.TokenExpirationDateTime,
    //         PreviousToken = refreshToken.PreviousToken,
    //         PreviousTokenExpirationDateTime = refreshToken.PreviousTokenExpirationDateTime,
    //         AppUserId = refreshToken.AppUserId,
    //         // AppUser = refreshToken.AppUser
    //     };
    // }
    public RefreshTokenMapper(IMapper mapper) : base(mapper)
    {
    }
}