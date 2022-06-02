﻿using App.DAL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers.Identity;

public class RefreshTokenMapper : BaseMapper<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken>
{
    public RefreshTokenMapper(IMapper mapper) : base(mapper)
    {
    }
    
    // public static App.BLL.DTO.Identity.RefreshToken MapToBll(RefreshToken refreshToken)
    // {
    //     // return new App.BLL.DTO.Identity.RefreshToken()
    //     // {
    //     //     Token = refreshToken.Token,
    //     //     TokenExpirationDateTime = refreshToken.TokenExpirationDateTime,
    //     //     PreviousToken = refreshToken.PreviousToken,
    //     //     PreviousTokenExpirationDateTime = refreshToken.PreviousTokenExpirationDateTime,
    //     //     AppUserId = refreshToken.AppUserId,
    //     //     AppUser = refreshToken.AppUser != null ? AppUserMapper.MapToBll(refreshToken.AppUser) : null
    //     // };
    //     
    // }
    
    // public static RefreshToken MapFromBll(App.BLL.DTO.Identity.RefreshToken refreshToken)
    // {
    //     return new RefreshToken()
    //     {
    //         Token = refreshToken.Token,
    //         TokenExpirationDateTime = refreshToken.TokenExpirationDateTime,
    //         PreviousToken = refreshToken.PreviousToken,
    //         PreviousTokenExpirationDateTime = refreshToken.PreviousTokenExpirationDateTime,
    //         AppUserId = refreshToken.AppUserId,
    //         AppUser = refreshToken.AppUser
    //     };
    // }
}