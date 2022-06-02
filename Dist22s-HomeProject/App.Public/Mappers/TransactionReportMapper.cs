using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using TransactionReport = App.Public.DTO.v1.TransactionReport;

namespace App.Public.Mappers;

public class TransactionReportMapper : BaseMapper<TransactionReport ,App.BLL.DTO.TransactionReport>
{
    public TransactionReportMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.TransactionReport MapToBll(TransactionReport transactionReport)
    {
        return new BLL.DTO.TransactionReport()
        {
            Id = transactionReport.Id,
            CreatedAt = transactionReport.CreatedAt,
            Comment = transactionReport.Comment ?? "",
            FirstName = transactionReport.FirstName,
            LastName = transactionReport.LastName,
            Email = transactionReport.Email,
            TotalPrice = transactionReport.TotalPrice,
            InvoiceId = transactionReport.InvoiceId,
            ProductOrders = transactionReport.ProductOrders != null ? transactionReport.ProductOrders.Select(x => ProductOrdersMapper.MapToBll(x)).ToList() : new List<ProductOrders>()
            // Order = transactionReport.Order != null ? OrderMapper.MapToBll(transactionReport.Order) : null
        };
    }
    
    public static TransactionReport MapFromBll(BLL.DTO.TransactionReport transactionReport)
    {
        return new TransactionReport()
        {
            Id = transactionReport.Id,
            CreatedAt = transactionReport.CreatedAt,
            Comment = transactionReport.Comment ?? "",
            FirstName = transactionReport.FirstName,
            LastName = transactionReport.LastName,
            Email = transactionReport.Email,
            TotalPrice = transactionReport.TotalPrice,
            InvoiceId = transactionReport.InvoiceId,
            ProductOrders = transactionReport.ProductOrders != null ? transactionReport.ProductOrders.Select(x => ProductOrdersMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.ProductOrders>()
            // Order = transactionReport.Order != null ? OrderMapper.MapFromBll(transactionReport.Order) : null
        };
    }
}