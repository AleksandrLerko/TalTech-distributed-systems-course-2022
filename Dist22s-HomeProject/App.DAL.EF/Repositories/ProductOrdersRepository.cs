using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductOrdersRepository : BaseEntityRepository<App.DAL.DTO.ProductOrders, ProductOrders, AppDbContext>, IProductOrdersRepository
{
    public ProductOrdersRepository(AppDbContext dbContext, IMapper<DTO.ProductOrders, ProductOrders> mapper) : base(dbContext, mapper)
    {
    }

    public async override Task<IEnumerable<DTO.ProductOrders>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery();
        query = query
            .Include(p => p.Order)
            .Include(p => p.Product)
            .Include(p => p.TransactionReport);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async override Task<DTO.ProductOrders?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery();
        query = query
            .Include(p => p.Order)
            .Include(p => p.Product)
            .Include(p => p.TransactionReport);
    
        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}