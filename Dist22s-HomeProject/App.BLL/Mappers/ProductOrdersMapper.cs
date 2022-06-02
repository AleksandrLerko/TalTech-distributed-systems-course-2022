using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductOrdersMapper : BaseMapper<App.BLL.DTO.ProductOrders, App.DAL.DTO.ProductOrders>
{
    public ProductOrdersMapper(IMapper mapper) : base(mapper)
    {
    }

}