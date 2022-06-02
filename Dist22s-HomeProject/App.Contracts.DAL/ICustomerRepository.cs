using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ICustomerRepository : IEntityRepository<App.DAL.DTO.Customer>, ICustomerRepositoryCustom<App.DAL.DTO.Customer>
{
    
}
public interface ICustomerRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}