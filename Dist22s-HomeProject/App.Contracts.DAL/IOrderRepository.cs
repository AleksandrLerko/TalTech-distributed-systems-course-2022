using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IOrderRepository : IEntityRepository<App.DAL.DTO.Order>, IOrderRepositoryCustom<App.DAL.DTO.Order>
{
}

public interface IOrderRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}