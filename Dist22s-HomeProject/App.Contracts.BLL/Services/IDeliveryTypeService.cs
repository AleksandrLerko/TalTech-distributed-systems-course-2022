using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IDeliveryTypeService : IEntityService<App.BLL.DTO.DeliveryType>, IDeliveryTypeRepositoryCustom<App.BLL.DTO.DeliveryType>
{
    
}