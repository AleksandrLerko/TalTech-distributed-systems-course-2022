using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ISpecificationTypeRepository : IEntityRepository<App.DAL.DTO.SpecificationType>, ISpecificationTypeRepositoryCustom<App.DAL.DTO.SpecificationType>
{
    
}

public interface ISpecificationTypeRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}