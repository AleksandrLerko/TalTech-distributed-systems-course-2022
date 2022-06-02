using App.Domain;
using AutoMapper;
using Base.Contracts;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class InvoiceMapper : BaseMapper<App.DAL.DTO.Invoice, Invoice>
{
    public InvoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}