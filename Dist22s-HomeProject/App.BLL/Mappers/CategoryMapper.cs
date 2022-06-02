using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CategoryMapper : BaseMapper<App.BLL.DTO.Category, App.DAL.DTO.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}