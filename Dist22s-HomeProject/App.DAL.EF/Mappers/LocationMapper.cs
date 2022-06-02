using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class LocationMapper : BaseMapper<App.DAL.DTO.Location, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}