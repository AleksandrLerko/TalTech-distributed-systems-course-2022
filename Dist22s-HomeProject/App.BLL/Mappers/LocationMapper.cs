using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class LocationMapper : BaseMapper<App.BLL.DTO.Location, App.DAL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}