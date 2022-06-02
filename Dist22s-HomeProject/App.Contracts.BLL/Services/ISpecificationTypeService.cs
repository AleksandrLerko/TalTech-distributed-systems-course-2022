using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISpecificationTypeService : IEntityService<App.BLL.DTO.SpecificationType>, ISpecificationTypeRepositoryCustom<App.BLL.DTO.SpecificationType>
{
    
}