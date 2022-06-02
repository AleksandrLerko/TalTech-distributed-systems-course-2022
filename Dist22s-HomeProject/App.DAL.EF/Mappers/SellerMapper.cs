using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class SellerMapper : BaseMapper<App.DAL.DTO.Seller, Seller>
{
    public SellerMapper(IMapper mapper) : base(mapper)
    {
    }
}