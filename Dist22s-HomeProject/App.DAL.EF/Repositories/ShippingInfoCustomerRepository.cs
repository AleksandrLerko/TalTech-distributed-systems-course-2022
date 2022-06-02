using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShippingInfoCustomerRepository : BaseEntityRepository<App.DAL.DTO.ShippingInfoCustomer, ShippingInfoCustomer, AppDbContext>, IShippingInfoCustomerRepository
{
    public ShippingInfoCustomerRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.ShippingInfoCustomer, ShippingInfoCustomer> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.ShippingInfoCustomer>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Customer)
            .Include(s => s.ShippingInfo);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }
    

    public override async Task<DTO.ShippingInfoCustomer?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(s => s.Customer)
            .Include(s => s.ShippingInfo);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}