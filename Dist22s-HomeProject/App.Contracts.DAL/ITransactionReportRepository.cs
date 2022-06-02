using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface ITransactionReportRepository : IEntityRepository<App.DAL.DTO.TransactionReport>, ITransactionReportRepositoryCustom<App.DAL.DTO.TransactionReport>
{
    
}

public interface ITransactionReportRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}