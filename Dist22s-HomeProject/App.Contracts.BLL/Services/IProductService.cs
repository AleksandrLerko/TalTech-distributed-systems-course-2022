using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IProductService : IEntityService<App.BLL.DTO.Product>, IProductRepositoryCustom<App.BLL.DTO.Product>
{
    
}