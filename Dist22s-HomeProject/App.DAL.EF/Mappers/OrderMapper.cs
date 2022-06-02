using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class OrderMapper : BaseMapper<App.DAL.DTO.Order, Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}