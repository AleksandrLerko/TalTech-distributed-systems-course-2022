using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CategoryTypeMapper : BaseMapper<App.BLL.DTO.CategoryType, App.DAL.DTO.CategoryType>
{
    public CategoryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}