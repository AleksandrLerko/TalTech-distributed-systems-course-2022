using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InStockRepository : BaseEntityRepository<App.DAL.DTO.InStock, InStock, AppDbContext>, IInStockRepository
{
    public InStockRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.InStock, InStock> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.InStock>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query.Include(i => i.Product);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<DTO.InStock?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query.Include(i => i.Product);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}