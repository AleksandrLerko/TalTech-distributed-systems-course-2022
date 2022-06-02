using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IFeedbackRepository : IEntityRepository<App.DAL.DTO.Feedback>, IFeedbackRepositoryCustom<App.DAL.DTO.Feedback>
{
    // Task<IEnumerable<Feedback>> GetAllAsync(Guid userId, bool noTracking = true);
}
public interface IFeedbackRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}