using Base.Contracts.DAL;

namespace App.Contracts.DAL.Identity;

public interface IAppUserRepository : IEntityRepository<App.DAL.DTO.Identity.AppUser>, IAppUserRepositoryCustom<App.DAL.DTO.Identity.AppUser>
{
    
}

public interface IAppUserRepositoryCustom<TEntity>
{
    Task<TEntity> GetRefreshTokens(Guid userId, bool noTracking = true);

    Task<TEntity> RemoveToken(Guid userId, string token, bool noTracking = true);

    Task<TEntity> GetUserById(Guid userId, bool noTracking = true);
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}