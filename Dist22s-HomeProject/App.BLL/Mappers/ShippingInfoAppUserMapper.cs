using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class ShippingInfoAppUserMapper : BaseMapper<App.BLL.DTO.ShippingInfoAppUser, App.DAL.DTO.ShippingInfoAppUser>
{
    public ShippingInfoAppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}