using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShippingInfoRepository : BaseEntityRepository<App.DAL.DTO.ShippingInfo, ShippingInfo, AppDbContext>, IShippingInfoRepository
{
    public ShippingInfoRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.ShippingInfo, ShippingInfo> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<App.DAL.DTO.ShippingInfo>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.ShippingInfoAppUsers)
            .Include(s => s.Customer)
            .Include(s => s.Orders);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<App.DAL.DTO.ShippingInfo?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.ShippingInfoAppUsers)
            .Include(s => s.Customer)
            .Include(s => s.Orders);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}