using App.Contracts.DAL.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    ICategoryRepository Categories { get; }
    ICategoryTypeRepository CategoryTypes { get; }
    ICurrencyRepository Currencies { get; }
    ICustomerRepository Customers { get; }
    IDeliveryTypeRepository DeliveryTypes { get; }
    IFeedbackRepository Feedbacks { get; }
    IInStockRepository InStocks { get; }
    ILocationRepository Locations { get; }
    IOrderRepository Orders { get; }
    IPaymentTypeRepository PaymentTypes { get; }
    IProductRepository Products { get; }
    ISellerRepository Sellers { get; }
    IShippingInfoRepository ShippingInfos { get; }
    IShippingInfoAppUserRepository ShippingInfoAppUsers { get; }
    IShippingInfoCustomerRepository ShippingInfoCustomers { get; }
    ISpecificationRepository Specifications { get; }
    ISpecificationTypeRepository SpecificationTypes { get; }
    ITransactionReportRepository TransactionReports { get; }
    IPictureRepository Pictures { get; }
    
    IAppUserRepository AppUser { get; }
    
    IRefreshTokenRepository RefreshTokens { get; }
    
    IProductOrdersRepository ProductOrders { get; }
    
    IInvoiceRepository Invoice { get; }
}