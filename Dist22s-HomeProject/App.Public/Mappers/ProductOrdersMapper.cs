using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using CategoryType = App.BLL.DTO.CategoryType;
using Product = App.BLL.DTO.Product;

namespace App.Public.Mappers;

public class ProductOrdersMapper : BaseMapper<ProductOrders ,App.BLL.DTO.ProductOrders>
{
    public ProductOrdersMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.ProductOrders MapToBll(ProductOrders productOrders)
    {
        return new BLL.DTO.ProductOrders()
        {
            Id = productOrders.Id,
            OrderId = productOrders.OrderId,
            ProductId = productOrders.ProductId,
            TransactionReportId = productOrders.TransactionReportId
        };
    }
    
    public static ProductOrders MapFromBll(BLL.DTO.ProductOrders productOrders)
    {
        return new ProductOrders()
        {
            Id = productOrders.Id,
            OrderId = productOrders.OrderId,
            ProductId = productOrders.ProductId,
            TransactionReportId = productOrders.TransactionReportId
        };
    }
}