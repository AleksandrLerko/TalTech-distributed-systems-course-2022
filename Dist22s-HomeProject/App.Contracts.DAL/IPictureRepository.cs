using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPictureRepository : IEntityRepository<App.DAL.DTO.Picture>, IPictureRepositoryCustom<App.DAL.DTO.Picture>
{
    
}

public interface IPictureRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);

    Task<TEntity?> GetPictureByProductId(Guid productId, bool noTracking = true);
}