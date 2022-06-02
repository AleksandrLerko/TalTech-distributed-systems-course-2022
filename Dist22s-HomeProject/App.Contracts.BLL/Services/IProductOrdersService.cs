using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IProductOrdersService : IEntityService<App.BLL.DTO.ProductOrders>, IProductOrdersRepositoryCustom<App.BLL.DTO.ProductOrders>
{
    
}