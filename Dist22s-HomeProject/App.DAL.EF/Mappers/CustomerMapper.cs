using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class CustomerMapper : BaseMapper<App.DAL.DTO.Customer, Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}