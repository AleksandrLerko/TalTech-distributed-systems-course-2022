using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class DeliveryTypeMapper : BaseMapper<App.DAL.DTO.DeliveryType, DeliveryType>
{
    public DeliveryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}