using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SpecificationRepository : BaseEntityRepository<App.DAL.DTO.Specification, Specification, AppDbContext>, ISpecificationRepository
{
    public SpecificationRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Specification, Specification> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<DTO.Specification>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Product)
            .Include(s => s.SpecificationTypes);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<DTO.Specification?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Product)
            .Include(s => s.SpecificationTypes);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}