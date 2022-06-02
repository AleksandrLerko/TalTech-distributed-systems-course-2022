using AutoMapper;
using Base.DAL;


namespace App.BLL.Mappers;

public class CurrencyMapper : BaseMapper<App.BLL.DTO.Currency, App.DAL.DTO.Currency>
{
    public CurrencyMapper(IMapper mapper) : base(mapper)
    {
    }
}