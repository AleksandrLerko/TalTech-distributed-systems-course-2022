using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICustomerService : IEntityService<App.BLL.DTO.Customer>, ICustomerRepositoryCustom<App.BLL.DTO.Customer>
{
    
}