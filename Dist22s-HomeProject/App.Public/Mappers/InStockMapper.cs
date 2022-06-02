using AutoMapper;
using Base.DAL;
using InStock = App.Public.DTO.v1.InStock;

namespace App.Public.Mappers;

public class InStockMapper : BaseMapper<InStock ,App.BLL.DTO.InStock>
{
    public InStockMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.InStock MapToBll(InStock inStock)
    {
        return new BLL.DTO.InStock()
        {
            Id = inStock.Id,
            Quantity = inStock.Quantity,
            ProductId = inStock.ProductId,
            // Product = inStock.Product != null ? ProductMapper.MapToBll(inStock.Product) : null,
            // Location = inStock.Location != null ? LocationMapper.MapToBll(inStock.Location) : null
        };
    }
    
    public static InStock MapFromBll(BLL.DTO.InStock inStock)
    {
        return new InStock()
        {
            Id = inStock.Id,
            Quantity = inStock.Quantity,
            ProductId = inStock.ProductId,
            // Product = inStock.Product != null ? ProductMapper.MapFromBll(inStock.Product) : null,
            // Location = inStock.Location != null ? LocationMapper.MapFromBll(inStock.Location) : null
        };
    }
}