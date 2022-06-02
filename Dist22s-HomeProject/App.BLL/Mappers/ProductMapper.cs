using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductMapper : BaseMapper<App.BLL.DTO.Product, App.DAL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}