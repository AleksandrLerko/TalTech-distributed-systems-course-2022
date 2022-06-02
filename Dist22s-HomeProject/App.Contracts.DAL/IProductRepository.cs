using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IProductRepository : IEntityRepository<App.DAL.DTO.Product>, IProductRepositoryCustom<App.DAL.DTO.Product>
{
    
}

public interface IProductRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);

    Task<IEnumerable<TEntity>> GetProductsByCategory(Guid categoryId);
    Task<IEnumerable<TEntity>> GetProductByName(string productName);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}