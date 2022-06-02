using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IOrderService : IEntityService<App.BLL.DTO.Order>, IOrderRepositoryCustom<App.BLL.DTO.Order>
{
    
}