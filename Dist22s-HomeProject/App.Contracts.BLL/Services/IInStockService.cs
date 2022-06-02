using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IInStockService : IEntityService<App.BLL.DTO.InStock>, IInStockRepositoryCustom<App.BLL.DTO.InStock>
{
    
}