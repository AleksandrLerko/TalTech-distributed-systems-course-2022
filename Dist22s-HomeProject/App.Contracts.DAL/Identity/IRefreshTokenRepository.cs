using Base.Contracts.DAL;

namespace App.Contracts.DAL.Identity;


public interface IRefreshTokenRepository : IEntityRepository<App.DAL.DTO.Identity.RefreshToken>, IRefreshTokenRepositoryCustom<App.DAL.DTO.Identity.RefreshToken>
{

}

public interface IRefreshTokenRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetTokensByUserId(Guid userId, bool noTracking = true);
    // Task<TEntity> GetRefreshTokens(Guid userId, bool noTracking = true);
    //
    // Task<TEntity> RemoveToken(Guid userId, string token, bool noTracking = true);
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}
