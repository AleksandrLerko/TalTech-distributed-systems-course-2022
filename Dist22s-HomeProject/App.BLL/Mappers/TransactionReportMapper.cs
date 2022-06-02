using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class TransactionReportMapper : BaseMapper<App.BLL.DTO.TransactionReport, App.DAL.DTO.TransactionReport>
{
    public TransactionReportMapper(IMapper mapper) : base(mapper)
    {
    }
}