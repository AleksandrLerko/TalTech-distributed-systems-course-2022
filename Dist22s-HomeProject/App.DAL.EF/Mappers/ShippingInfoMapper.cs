using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class ShippingInfoMapper : BaseMapper<App.DAL.DTO.ShippingInfo, ShippingInfo>
{
    public ShippingInfoMapper(IMapper mapper) : base(mapper)
    {
    }
}