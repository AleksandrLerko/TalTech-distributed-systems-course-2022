using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class ShippingInfoAppUserMapper : BaseMapper<App.DAL.DTO.ShippingInfoAppUser, ShippingInfoAppUser>
{
    public ShippingInfoAppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}