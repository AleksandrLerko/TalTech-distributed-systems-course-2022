using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IDeliveryTypeRepository : IEntityRepository<App.DAL.DTO.DeliveryType>, IDeliveryTypeRepositoryCustom<App.DAL.DTO.DeliveryType>
{
    
}
public interface IDeliveryTypeRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}