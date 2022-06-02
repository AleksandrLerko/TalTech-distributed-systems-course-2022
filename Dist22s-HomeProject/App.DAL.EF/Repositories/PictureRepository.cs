using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class PictureRepository : BaseEntityRepository<App.DAL.DTO.Picture, Picture, AppDbContext>, IPictureRepository
{
    public PictureRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Picture, Picture> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.Picture>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Product);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    
    public override async Task<App.DAL.DTO.Picture?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Product);
    
        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }

    public async Task<DTO.Picture?> GetPictureByProductId(Guid productId, bool noTracking = true)
    {

        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Product)
            .Where(i => i.ProductId == productId);

        var res = await query.FirstOrDefaultAsync(i => i.ProductId == productId);
        return Mapper.Map(res);
    }
}