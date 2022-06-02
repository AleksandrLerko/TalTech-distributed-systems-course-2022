using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShippingInfoAppUserRepository : BaseEntityRepository<App.DAL.DTO.ShippingInfoAppUser, ShippingInfoAppUser, AppDbContext>, IShippingInfoAppUserRepository
{
    public ShippingInfoAppUserRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.ShippingInfoAppUser, ShippingInfoAppUser> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.ShippingInfoAppUser>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.ShippingInfo)
            .Where(o => o.AppUserId == userId);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<App.DAL.DTO.ShippingInfoAppUser?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.ShippingInfo);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}