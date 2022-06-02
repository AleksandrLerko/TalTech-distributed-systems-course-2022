using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Location = App.Public.DTO.v1.Location;

namespace App.Public.Mappers;

public class LocationMapper : BaseMapper<Location ,App.BLL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Location MapToBll(Location location)
    {
        return new BLL.DTO.Location()
        {
            Id = location.Id,
            LocationName = location.LocationName,
            InStocks = location.InStocks != null ? location.InStocks.Select(x => InStockMapper.MapToBll(x)).ToList() : new List<InStock>()
        };
    }
    
    public static Location MapFromBll(BLL.DTO.Location location)
    {
        return new Location()
        {
            Id = location.Id,
            LocationName = location.LocationName,
            InStocks = location.InStocks != null ? location.InStocks.Select(x => InStockMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.InStock>()
        };
    }
}