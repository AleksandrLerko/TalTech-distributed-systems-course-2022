using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TransactionReportRepository : BaseEntityRepository<App.DAL.DTO.TransactionReport, TransactionReport, AppDbContext>, ITransactionReportRepository
{
    public TransactionReportRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.TransactionReport, TransactionReport> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DTO.TransactionReport>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(t => t.ProductOrders)
            .Include(t => t.Invoice);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<DTO.TransactionReport?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(t => t.ProductOrders)
            .Include(t => t.Invoice);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}