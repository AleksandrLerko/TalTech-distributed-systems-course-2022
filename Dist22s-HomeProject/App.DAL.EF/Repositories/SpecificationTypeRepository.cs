using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SpecificationTypeRepository : BaseEntityRepository<App.DAL.DTO.SpecificationType, SpecificationType, AppDbContext>, ISpecificationTypeRepository
{
    public SpecificationTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.SpecificationType, SpecificationType> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.SpecificationType>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Specification);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<DTO.SpecificationType?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Specification);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}