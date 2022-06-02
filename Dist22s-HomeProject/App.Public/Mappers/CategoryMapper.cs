using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using CategoryType = App.BLL.DTO.CategoryType;
using Product = App.BLL.DTO.Product;

namespace App.Public.Mappers;

public class CategoryMapper : BaseMapper<Category ,App.BLL.DTO.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Category MapToBll(Category category)
    {
        return new BLL.DTO.Category()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Products =  category.Products != null ? category.Products.Select(x => ProductMapper.MapToBll(x)).ToList() : new List<Product>(),
            CategoryTypes = category.CategoryTypes != null ? category.CategoryTypes.Select(x => CategoryTypeMapper.MapToBll(x)).ToList() : new List<CategoryType>()
        };
    }
    
    public static Category MapFromBll(BLL.DTO.Category category)
    {
        return new Category()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Products =  category.Products != null ? category.Products.Select(x => ProductMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Product>(),
            CategoryTypes = category.CategoryTypes != null ? category.CategoryTypes.Select(x => CategoryTypeMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.CategoryType>()
        };
    }
}