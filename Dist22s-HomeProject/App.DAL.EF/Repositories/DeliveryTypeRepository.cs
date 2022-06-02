using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;


namespace App.DAL.EF.Repositories;

public class DeliveryTypeRepository : BaseEntityRepository<App.DAL.DTO.DeliveryType, DeliveryType, AppDbContext>, IDeliveryTypeRepository
{
    public DeliveryTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.DeliveryType, DeliveryType> mapper) : base(dbContext, mapper)
    {
    }
}