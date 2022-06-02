using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services.Identity;

public class RefreshTokenService : BaseEntityService<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken, IRefreshTokenRepository>, IRefreshTokenService
{
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper<RefreshToken, DAL.DTO.Identity.RefreshToken> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<RefreshToken>> GetTokensByUserId(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetTokensByUserId(userId, noTracking)).Select(x => Mapper.Map(x));
        return res!;
    }
}