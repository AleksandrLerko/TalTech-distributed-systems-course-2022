using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.Domain;
using App.Domain.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using AppRole = App.DAL.DTO.Identity.AppRole;
using RefreshToken = App.BLL.DTO.Identity.RefreshToken;

namespace App.DAL.EF.Repositories.Identity;

public class AppUserRepository : BaseEntityRepository<App.DAL.DTO.Identity.AppUser, AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Identity.AppUser, AppUser> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<DTO.Identity.AppUser> GetRefreshTokens(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        var resQuery = query.Where(a => a.Id == userId);
        resQuery = resQuery
            .Include(a => a.ShippingInfoAppUsers)
            .Include(a => a.RefreshTokens)
            .Include(a => a.Orders)
            .Include(a => a.Feedbacks);

        var res = await resQuery.FirstOrDefaultAsync();
        var mapped = Mapper.Map(res)!;
        return Mapper.Map(res)!;
    }

    // public async Task<DTO.Identity.AppUser> RemoveToken(Guid userId, RefreshToken userRefreshToken, bool noTracking = true)
    // {
    //     var query = CreateQuery(noTracking);
    //
    //     var appUserQuery = query.Where(a => a.Id == userId);
    //     
    //     var validAppUser = await appUserQuery.FirstOrDefaultAsync();
    //
    //     if (validAppUser != null)
    //     {
    //         validAppUser.RefreshTokens.Remove(userRefreshToken);
    //     }
    // }

    public async Task<DTO.Identity.AppUser> RemoveToken(Guid userId, string token, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        var appUserQuery = query.Where(a => a.Id == userId);
        
        var validAppUser = await appUserQuery.FirstOrDefaultAsync();

        if (validAppUser?.RefreshTokens != null)
        {
            foreach (var refreshToken in validAppUser.RefreshTokens)
            {
                if (refreshToken.Token.Equals(token))
                {
                    validAppUser.RefreshTokens.Remove(refreshToken);
                }
            }
        }

        return Mapper.Map(validAppUser)!;
    }

    public async Task<DTO.Identity.AppUser> GetUserById(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        var resQuery = query.Where(x => x.Id == userId);

        var res = (await resQuery.FirstOrDefaultAsync());

        return Mapper.Map(res)!;
    }
}