using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class OrderMapper : BaseMapper<App.BLL.DTO.Order, App.DAL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}