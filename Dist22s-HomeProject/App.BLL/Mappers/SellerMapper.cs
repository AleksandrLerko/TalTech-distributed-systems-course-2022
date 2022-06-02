using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class SellerMapper : BaseMapper<App.BLL.DTO.Seller, App.DAL.DTO.Seller>
{
    public SellerMapper(IMapper mapper) : base(mapper)
    {
    }
}