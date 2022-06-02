using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ISpecificationRepository : IEntityRepository<App.DAL.DTO.Specification>, ISpecificationRepositoryCustom<App.DAL.DTO.Specification>
{
    
}

public interface ISpecificationRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}