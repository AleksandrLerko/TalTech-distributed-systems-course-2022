using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IInvoiceService : IEntityService<App.BLL.DTO.Invoice>, IInvoiceRepositoryCustom<App.BLL.DTO.Invoice>
{
    
}