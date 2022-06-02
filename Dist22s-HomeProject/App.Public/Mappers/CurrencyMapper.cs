using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using Product = App.BLL.DTO.Product;

namespace App.Public.Mappers;

public class CurrencyMapper : BaseMapper<Currency ,App.BLL.DTO.Currency>
{
    public CurrencyMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Currency MapToBll(Currency currency)
    {
        return new BLL.DTO.Currency()
        {
            Id = currency.Id,
            CurrencyName = currency.CurrencyName,
            Products = currency.Products != null ? currency.Products.Select(x => ProductMapper.MapToBll(x)).ToList() : new List<Product>()
        };
    }
    
    public static Currency MapFromBll(BLL.DTO.Currency currency)
    {
        return new Currency()
        {
            Id = currency.Id,
            CurrencyName = currency.CurrencyName,
            Products = currency.Products != null ? currency.Products.Select(x => ProductMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Product>()
        };
    }
}