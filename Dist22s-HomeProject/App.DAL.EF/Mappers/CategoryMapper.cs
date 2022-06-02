using App.Domain;
using AutoMapper;
using Base.Contracts;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class CategoryMapper : BaseMapper<App.DAL.DTO.Category, Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}