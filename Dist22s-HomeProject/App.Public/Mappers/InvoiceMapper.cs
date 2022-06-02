using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using CategoryType = App.BLL.DTO.CategoryType;
using Product = App.BLL.DTO.Product;

namespace App.Public.Mappers;

public class InvoiceMapper : BaseMapper<Invoice ,App.BLL.DTO.Invoice>
{
    public InvoiceMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Invoice MapToBll(Invoice invoice)
    {
        return new BLL.DTO.Invoice()
        {
            Id = invoice.Id,
            Date = invoice.Date,
            FirstName = invoice.FirstName,
            LastName = invoice.LastName,
            Email = invoice.Email,
            PaymentMethodName = invoice.PaymentMethodName,
            DeliveryMethodName = invoice.DeliveryMethodName,
            FullAddress = invoice.FullAddress,
            FinalPrice = invoice.FinalPrice,
            TransactionReport = invoice.TransactionReport != null ? TransactionReportMapper.MapToBll(invoice.TransactionReport) : null
        };
    }
    
    public static Invoice MapFromBll(BLL.DTO.Invoice invoice)
    {
        return new Invoice()
        {
            Id = invoice.Id,
            Date = invoice.Date,
            FirstName = invoice.FirstName,
            LastName = invoice.LastName,
            Email = invoice.Email,
            PaymentMethodName = invoice.PaymentMethodName,
            DeliveryMethodName = invoice.DeliveryMethodName,
            FullAddress = invoice.FullAddress,
            FinalPrice = invoice.FinalPrice,
            TransactionReport = invoice.TransactionReport != null ? TransactionReportMapper.MapFromBll(invoice.TransactionReport) : null
        };
    }
}