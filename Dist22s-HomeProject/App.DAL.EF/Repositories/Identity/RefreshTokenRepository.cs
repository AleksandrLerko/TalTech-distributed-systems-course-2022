using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.Domain;
using App.Domain.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories.Identity;

public class RefreshTokenRepository : BaseEntityRepository<App.DAL.DTO.Identity.RefreshToken, RefreshToken, AppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext dbContext, IMapper<DTO.Identity.RefreshToken, RefreshToken> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.Identity.RefreshToken>> GetTokensByUserId(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery();

        var resQuery = query.Where(t => t.AppUserId == userId);

        var res = await resQuery.ToListAsync();

        return res.Select(x => Mapper.Map(x))!;
    }
}