using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class ShippingInfoCustomerMapper : BaseMapper<App.DAL.DTO.ShippingInfoCustomer, ShippingInfoCustomer>
{
    public ShippingInfoCustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}