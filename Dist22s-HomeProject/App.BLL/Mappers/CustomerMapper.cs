using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class CustomerMapper : BaseMapper<App.BLL.DTO.Customer, App.DAL.DTO.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}