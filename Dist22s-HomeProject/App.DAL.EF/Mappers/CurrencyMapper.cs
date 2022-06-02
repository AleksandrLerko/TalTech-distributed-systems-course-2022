using App.Domain;
using AutoMapper;
using Base.Contracts;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class CurrencyMapper : BaseMapper<App.DAL.DTO.Currency, Currency>
{
    public CurrencyMapper(IMapper mapper) : base(mapper)
    {
    }
}