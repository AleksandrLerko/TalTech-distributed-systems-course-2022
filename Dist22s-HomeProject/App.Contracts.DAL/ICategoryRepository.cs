using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICategoryRepository : IEntityRepository<App.DAL.DTO.Category>, ICategoryRepositoryCustom<App.DAL.DTO.Category>
{
    
}

public interface ICategoryRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}