using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ILocationService : IEntityService<App.BLL.DTO.Location>, ILocationRepositoryCustom<App.BLL.DTO.Location>
{
    
}