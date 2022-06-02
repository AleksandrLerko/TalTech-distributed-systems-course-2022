using App.DAL.DTO;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using ProductOrders = App.Domain.ProductOrders;

namespace App.DAL.EF.Mappers;

public class ProductOrdersMapper : BaseMapper<App.DAL.DTO.ProductOrders, ProductOrders>
{
    public ProductOrdersMapper(IMapper mapper) : base(mapper)
    {
    }

}