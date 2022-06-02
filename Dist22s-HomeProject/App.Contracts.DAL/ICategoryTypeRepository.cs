using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICategoryTypeRepository : IEntityRepository<App.DAL.DTO.CategoryType>, ICategoryTypeRepositoryCustom<App.DAL.DTO.CategoryType>
{
}

public interface ICategoryTypeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}