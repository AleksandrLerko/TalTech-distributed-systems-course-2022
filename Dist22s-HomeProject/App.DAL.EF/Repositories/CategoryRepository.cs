
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseEntityRepository<App.DAL.DTO.Category, Category, AppDbContext>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Category, Category> mapper) : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<DTO.Category>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(c => c.Products);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public override async Task<DTO.Category?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(c => c.Products);
    
        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}