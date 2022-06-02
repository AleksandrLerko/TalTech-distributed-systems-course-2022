using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IInStockRepository : IEntityRepository<App.DAL.DTO.InStock>, IInStockRepositoryCustom<App.DAL.DTO.InStock>
{
    
}

public interface IInStockRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}