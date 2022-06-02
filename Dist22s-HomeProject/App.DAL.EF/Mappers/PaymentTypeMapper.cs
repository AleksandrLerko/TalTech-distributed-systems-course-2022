using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class PaymentTypeMapper : BaseMapper<App.DAL.DTO.PaymentType, PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}