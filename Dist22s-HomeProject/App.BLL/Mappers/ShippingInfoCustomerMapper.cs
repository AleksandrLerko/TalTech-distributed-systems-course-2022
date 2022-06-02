using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class ShippingInfoCustomerMapper : BaseMapper<App.BLL.DTO.ShippingInfoCustomer, App.DAL.DTO.ShippingInfoCustomer>
{
    public ShippingInfoCustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}