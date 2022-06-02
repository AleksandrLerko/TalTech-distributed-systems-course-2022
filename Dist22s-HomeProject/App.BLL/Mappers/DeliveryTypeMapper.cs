using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class DeliveryTypeMapper : BaseMapper<App.BLL.DTO.DeliveryType, App.DAL.DTO.DeliveryType>
{
    public DeliveryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}