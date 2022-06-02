using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Identity;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services.Identity;

public class AppUserService : BaseEntityService<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser, IAppUserRepository>, IAppUserService
{
    public AppUserService(IAppUserRepository repository, IMapper<AppUser, DAL.DTO.Identity.AppUser> mapper) : base(repository, mapper)
    {
    }

    public async Task<AppUser> GetRefreshTokens(Guid userId, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.GetRefreshTokens(userId, noTracking));
        return res!;
    }

    public async Task<AppUser> RemoveToken(Guid userId, string token, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(userId, noTracking));
        return res!;
    }

    public async Task<AppUser> GetUserById(Guid userId, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.GetUserById(userId, noTracking));
        return res!;
    }
}