using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ISellerRepository : IEntityRepository<App.DAL.DTO.Seller>, ISellerRepositoryCustom<App.DAL.DTO.Seller>
{
    
}

public interface ISellerRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}