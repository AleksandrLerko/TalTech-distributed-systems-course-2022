using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITransactionReportService : IEntityService<App.BLL.DTO.TransactionReport>, ITransactionReportRepositoryCustom<App.BLL.DTO.TransactionReport>
{
    
}