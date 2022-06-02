using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICategoryService : IEntityService<App.BLL.DTO.Category>, ICategoryRepositoryCustom<App.BLL.DTO.Category>
{
    
}