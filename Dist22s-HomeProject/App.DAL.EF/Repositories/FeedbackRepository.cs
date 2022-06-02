using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class FeedbackRepository : BaseEntityRepository<App.DAL.DTO.Feedback, Feedback, AppDbContext>, IFeedbackRepository
{
    public FeedbackRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Feedback, Feedback> mapper) : base(dbContext, mapper)
    {   
    }

    public async Task<IEnumerable<App.DAL.DTO.Feedback>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.Product)
            .Where(o => o.AppUserId == userId);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }
    
    public override async Task<App.DAL.DTO.Feedback?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.Product);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}