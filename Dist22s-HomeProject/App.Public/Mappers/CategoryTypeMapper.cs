using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class CategoryTypeMapper : BaseMapper<CategoryType ,App.BLL.DTO.CategoryType>
{
    public CategoryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.CategoryType MapToBll(CategoryType categoryType)
    {
        return new BLL.DTO.CategoryType()
        {
            Id = categoryType.Id,
            TypeName = categoryType.TypeName,
            CategoryId = categoryType.CategoryId
        };
    }
    
    public static CategoryType MapFromBll(BLL.DTO.CategoryType categoryType)
    {
        return new CategoryType()
        {
            Id = categoryType.Id,
            TypeName = categoryType.TypeName,
            CategoryId = categoryType.CategoryId
        };
    }
}