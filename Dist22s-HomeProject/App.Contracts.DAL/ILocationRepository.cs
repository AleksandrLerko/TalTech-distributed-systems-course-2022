using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ILocationRepository : IEntityRepository<App.DAL.DTO.Location>, ILocationRepositoryCustom<App.DAL.DTO.Location>
{
    
}

public interface ILocationRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}