using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IInvoiceRepository : IEntityRepository<App.DAL.DTO.Invoice>, IInvoiceRepositoryCustom<App.DAL.DTO.Invoice>
{
    
}

public interface IInvoiceRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
}