using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services.Identity;

public interface IRefreshTokenService : IEntityService<App.BLL.DTO.Identity.RefreshToken>, IRefreshTokenRepositoryCustom<App.BLL.DTO.Identity.RefreshToken>
{
    
}