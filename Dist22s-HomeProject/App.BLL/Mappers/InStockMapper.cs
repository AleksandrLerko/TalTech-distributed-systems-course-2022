using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class InStockMapper : BaseMapper<App.BLL.DTO.InStock, App.DAL.DTO.InStock>
{
    public InStockMapper(IMapper mapper) : base(mapper)
    {
    }
}