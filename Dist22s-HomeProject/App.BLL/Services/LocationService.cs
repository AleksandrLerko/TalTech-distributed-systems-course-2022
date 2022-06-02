using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class LocationService : BaseEntityService<App.BLL.DTO.Location, App.DAL.DTO.Location, ILocationRepository>, ILocationService
{
    public LocationService(ILocationRepository repository, IMapper<Location, DAL.DTO.Location> mapper) : base(repository, mapper)
    {
    }
}