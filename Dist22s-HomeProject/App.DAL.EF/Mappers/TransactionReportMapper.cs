using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class TransactionReportMapper : BaseMapper<App.DAL.DTO.TransactionReport, TransactionReport>
{
    public TransactionReportMapper(IMapper mapper) : base(mapper)
    {
    }
}