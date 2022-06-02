using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IShippingInfoAppUserService : IEntityService<App.BLL.DTO.ShippingInfoAppUser>, IShippingInfoAppUserRepositoryCustom<App.BLL.DTO.ShippingInfoAppUser>
{
    
}