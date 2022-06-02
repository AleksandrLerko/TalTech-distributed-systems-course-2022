using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IShippingInfoCustomerService : IEntityService<App.BLL.DTO.ShippingInfoCustomer>, IShippingInfoCustomerRepositoryCustom<App.BLL.DTO.ShippingInfoCustomer>
{
    
}