using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IProductOrdersRepository : IEntityRepository<App.DAL.DTO.ProductOrders>, IProductOrdersRepositoryCustom<App.DAL.DTO.ProductOrders>
{
    
}

public interface IProductOrdersRepositoryCustom<TEntity>
{
    
}