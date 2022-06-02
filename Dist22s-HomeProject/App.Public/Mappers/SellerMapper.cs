using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Seller = App.Public.DTO.v1.Seller;

namespace App.Public.Mappers;

public class SellerMapper : BaseMapper<Seller ,App.BLL.DTO.Seller>
{
    public SellerMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Seller MapToBll(Seller seller)
    {
        return new BLL.DTO.Seller()
        {
            Id = seller.Id,
            SellerName = seller.SellerName,
            Products = seller.Products != null ? seller.Products.Select(x => ProductMapper.MapToBll(x)).ToList() : new List<Product>()
        };
    }
    
    public static Seller MapFromBll(BLL.DTO.Seller seller)
    {
        return new Seller()
        {
            Id = seller.Id,
            SellerName = seller.SellerName,
            Products = seller.Products != null ? seller.Products.Select(x => ProductMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Product>()
        };
    }
}