using System.Collections;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    ICategoryService Categories { get; }
    ICategoryTypeService CategoryTypes { get; }
    ICurrencyService Currencies { get; }
    ICustomerService Customers { get; }
    IDeliveryTypeService DeliveryTypes { get; }
    IFeedbackService Feedbacks { get; }
    IInStockService InStocks { get; }
    ILocationService Locations { get; }
    IOrderService Orders { get; }
    IPaymentTypeService PaymentTypes { get; }
    IProductService Products { get; }
    ISellerService Sellers { get; }
    IShippingInfoService ShippingInfos { get; }
    IShippingInfoAppUserService ShippingInfoAppUsers { get; }
    IShippingInfoCustomerService ShippingInfoCustomers { get; }
    ISpecificationService Specifications { get; }
    ISpecificationTypeService SpecificationTypes { get; }
    ITransactionReportService TransactionReports { get; }
    IPictureService Pictures { get; }
    
    IAppUserService AppUsers { get; }
    IRefreshTokenService RefreshTokens { get; }
    
    IProductOrdersService ProductOrders { get; }
    IInvoiceService Invoice { get; }
}