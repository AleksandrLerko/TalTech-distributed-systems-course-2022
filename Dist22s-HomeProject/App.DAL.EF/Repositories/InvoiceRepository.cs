using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InvoiceRepository : BaseEntityRepository<App.DAL.DTO.Invoice, Invoice, AppDbContext>, IInvoiceRepository
{
    public InvoiceRepository(AppDbContext dbContext, IMapper<DTO.Invoice, Invoice> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<DTO.Invoice>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(i => i.TransactionReport)
            .ThenInclude(t => t!.ProductOrders);
        var res = (await query.ToListAsync()).Select(r => Mapper.Map(r)!);

        return res;
    }

    public override async Task<DTO.Invoice?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query
            .Include(i => i.TransactionReport)
            .ThenInclude(t => t!.ProductOrders);

        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }
}