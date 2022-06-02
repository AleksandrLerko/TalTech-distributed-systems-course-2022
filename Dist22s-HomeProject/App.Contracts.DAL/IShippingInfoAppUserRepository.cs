using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IShippingInfoAppUserRepository : IEntityRepository<App.DAL.DTO.ShippingInfoAppUser>, IShippingInfoAppUserRepositoryCustom<App.DAL.DTO.ShippingInfoAppUser>
{
    // Task<IEnumerable<ShippingInfoAppUser>> GetAllAsync(Guid userId, bool noTracking = true);
}

public interface IShippingInfoAppUserRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}