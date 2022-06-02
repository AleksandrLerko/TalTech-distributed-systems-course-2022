using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class ProductMapper : BaseMapper<App.DAL.DTO.Product, Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}