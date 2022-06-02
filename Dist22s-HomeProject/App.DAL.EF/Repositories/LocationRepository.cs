using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;


namespace App.DAL.EF.Repositories;

public class LocationRepository : BaseEntityRepository<App.DAL.DTO.Location, Location, AppDbContext>, ILocationRepository
{
    public LocationRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Location, Location> mapper) : base(dbContext, mapper)
    {
    }
}