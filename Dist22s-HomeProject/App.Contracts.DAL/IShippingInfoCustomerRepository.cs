using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IShippingInfoCustomerRepository : IEntityRepository<App.DAL.DTO.ShippingInfoCustomer>, IShippingInfoCustomerRepositoryCustom<App.DAL.DTO.ShippingInfoCustomer>
{
    
}

public interface IShippingInfoCustomerRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}