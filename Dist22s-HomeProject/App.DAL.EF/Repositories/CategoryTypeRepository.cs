using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CategoryTypeRepository : BaseEntityRepository<App.DAL.DTO.CategoryType, CategoryType, AppDbContext>, ICategoryTypeRepository
{
    public CategoryTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.CategoryType, CategoryType> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.CategoryType>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(c => c.Category);

        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public override async Task<App.DAL.DTO.CategoryType?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(c => c.Category);

        var res = await query.FirstOrDefaultAsync(q => q.Id == id);
        return Mapper.Map(res);
    }
}