using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISellerService : IEntityService<App.BLL.DTO.Seller>, ISellerRepositoryCustom<App.BLL.DTO.Seller>
{
    
}