using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<App.DAL.DTO.Order, Order, AppDbContext>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Order, Order> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Order>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.Customer)
            .Include(o => o.DeliveryType)
            .Include(o => o.ProductOrders)
            .Where(o => o.AppUserId == userId);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<App.DAL.DTO.Order?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(o => o.AppUser)
            .Include(o => o.Customer)
            .Include(o => o.DeliveryType)
            .Include(o => o.ProductOrders);
    
        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }

}