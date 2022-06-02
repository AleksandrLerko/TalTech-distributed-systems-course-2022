using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IShippingInfoRepository : IEntityRepository<App.DAL.DTO.ShippingInfo>, IShippingInfoRepositoryCustom<App.DAL.DTO.ShippingInfo>
{
    
}

public interface IShippingInfoRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}