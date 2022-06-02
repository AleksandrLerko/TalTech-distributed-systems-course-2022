using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class InStockMapper : BaseMapper<App.DAL.DTO.InStock, InStock>
{
    public InStockMapper(IMapper mapper) : base(mapper)
    {
    }
}