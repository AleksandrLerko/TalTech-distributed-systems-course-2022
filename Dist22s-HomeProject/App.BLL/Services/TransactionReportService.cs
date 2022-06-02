using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class TransactionReportService : BaseEntityService<App.BLL.DTO.TransactionReport,
        App.DAL.DTO.TransactionReport,
        ITransactionReportRepository>,
    ITransactionReportService
{
    public TransactionReportService(ITransactionReportRepository repository, IMapper<TransactionReport, DAL.DTO.TransactionReport> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<TransactionReport>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<TransactionReport?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}