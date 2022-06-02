using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICategoryTypeService : IEntityService<App.BLL.DTO.CategoryType>, ICategoryTypeRepositoryCustom<App.BLL.DTO.CategoryType>
{
    // add custom stuff
    
}