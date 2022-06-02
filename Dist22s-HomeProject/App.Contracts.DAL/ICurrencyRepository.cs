using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICurrencyRepository : IEntityRepository<App.DAL.DTO.Currency>, ICurrencyRepositoryCustom<App.DAL.DTO.Currency>
{
    // write your custom methods here
}

public interface ICurrencyRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}