using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class DeliveryTypeService : BaseEntityService<App.BLL.DTO.DeliveryType, App.DAL.DTO.DeliveryType, IDeliveryTypeRepository>, IDeliveryTypeService
{
    public DeliveryTypeService(IDeliveryTypeRepository repository, IMapper<DeliveryType, DAL.DTO.DeliveryType> mapper) : base(repository, mapper)
    {
    }
}