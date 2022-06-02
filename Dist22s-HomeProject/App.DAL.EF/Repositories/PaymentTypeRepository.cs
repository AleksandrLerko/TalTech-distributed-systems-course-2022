using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;


namespace App.DAL.EF.Repositories;

public class PaymentTypeRepository : BaseEntityRepository<App.DAL.DTO.PaymentType, PaymentType, AppDbContext>, IPaymentTypeRepository
{
    public PaymentTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.PaymentType, PaymentType> mapper) : base(dbContext, mapper)
    {
    }
}