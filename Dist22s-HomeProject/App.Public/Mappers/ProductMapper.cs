using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using InStock = App.BLL.DTO.InStock;
using Order = App.BLL.DTO.Order;
using Picture = App.BLL.DTO.Picture;

namespace App.Public.Mappers;

public class ProductMapper : BaseMapper<Product ,App.BLL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Product MapToBll(Product product)
    {
        var res = new BLL.DTO.Product()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Description =  product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            // Category = product.Category != null ? CategoryMapper.MapToBll(product.Category!) : null,
            CurrencyId = product.CurrencyId,
            // Currency = product.Currency != null ? CurrencyMapper.MapToBll(product.Currency!) : null,
            SellerId = product.SellerId,
            // Seller = product.Seller != null ? SellerMapper.MapToBll(product.Seller!) : null,
            Picture = product.Picture != null ? PictureMapper.MapToBll(product.Picture) : null,
            InStocks = product.InStocks != null ? InStockMapper.MapToBll(product.InStocks) : null,
            Specifications = product.Specifications != null ? product.Specifications.Select(x => SpecificationMapper.MapToBll(x)).ToList() : new List<BLL.DTO.Specification>(),
            Feedbacks = product.Feedbacks != null ? product.Feedbacks.Select(x => FeedbackMapper.MapToBll(x)).ToList() : new List<BLL.DTO.Feedback>(),
            ProductOrders = product.ProductOrders != null ? product.ProductOrders.Select(x => ProductOrdersMapper.MapToBll(x)).ToList() : new List<BLL.DTO.ProductOrders>()
        };
        return res;
    }
    
    public static Product MapFromBll(BLL.DTO.Product product)
    {
        return new Product()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Description =  product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            // Category = product.Category != null ? CategoryMapper.MapFromBll(product.Category!) : null,
            CurrencyId = product.CurrencyId,
            // Currency = product.Currency != null ? CurrencyMapper.MapFromBll(product.Currency!) : null,
            SellerId = product.SellerId,
            // Seller = product.Seller != null ? SellerMapper.MapFromBll(product.Seller!) : null,
            Picture = product.Picture != null ? PictureMapper.MapFromBll(product.Picture) : null,
            InStocks = product.InStocks != null ? InStockMapper.MapFromBll(product.InStocks) : null,
            Specifications = product.Specifications != null ? product.Specifications.Select(x => SpecificationMapper.MapFromBll(x)).ToList() : new List<Specification>(),
            Feedbacks = product.Feedbacks != null ? product.Feedbacks.Select(x => FeedbackMapper.MapFromBll(x)).ToList() : new List<Feedback>(),
            ProductOrders = product.ProductOrders != null ? product.ProductOrders.Select(x => ProductOrdersMapper.MapFromBll(x)).ToList() : new List<ProductOrders>()
        };
    }
}