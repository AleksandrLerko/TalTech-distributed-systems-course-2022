using App.Domain;
using AutoMapper;
using Base.Contracts;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class CategoryTypeMapper : BaseMapper<App.DAL.DTO.CategoryType, CategoryType>
{
    public CategoryTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}