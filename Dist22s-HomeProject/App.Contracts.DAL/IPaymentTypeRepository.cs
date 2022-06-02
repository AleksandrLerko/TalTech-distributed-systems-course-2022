using Base.Contracts.DAL;


namespace App.Contracts.DAL;

public interface IPaymentTypeRepository : IEntityRepository<App.DAL.DTO.PaymentType>, IPaymentTypeRepositoryCustom<App.DAL.DTO.PaymentType>
{
    
}

public interface IPaymentTypeRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    // Task<TEntity?> FirstOrDefaultAsync(Guid id, bool noTracking = true);
    // Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true);
}