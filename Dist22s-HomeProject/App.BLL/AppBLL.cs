using App.BLL.Mappers;
using App.BLL.Mappers.Identity;
using App.BLL.Services;
using App.BLL.Services.Identity;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL: BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    protected readonly AutoMapper.IMapper _mapper;
    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private ICategoryService? _categories;
    public ICategoryService Categories =>
        _categories ??= new CategoryService(UnitOfWork.Categories, new CategoryMapper(_mapper));
    
    private ICategoryTypeService? _categoryTypes;
    public ICategoryTypeService CategoryTypes =>
        _categoryTypes ??= new CategoryTypeService(UnitOfWork.CategoryTypes, new CategoryTypeMapper(_mapper));
    
    private ICurrencyService? _currencies;
    public ICurrencyService Currencies =>
        _currencies ??= new CurrencyService(UnitOfWork.Currencies, new CurrencyMapper(_mapper));

    private ICustomerService? _customers;
    public ICustomerService Customers =>
        _customers ??= new CustomerService(UnitOfWork.Customers, new CustomerMapper(_mapper));

    private IDeliveryTypeService? _deliveryTypes;
    public IDeliveryTypeService DeliveryTypes =>
        _deliveryTypes ??= new DeliveryTypeService(UnitOfWork.DeliveryTypes, new DeliveryTypeMapper(_mapper));

    private IFeedbackService? _feedbacks;
    public IFeedbackService Feedbacks =>
        _feedbacks ??= new FeedbackService(UnitOfWork.Feedbacks, new FeedbackMapper(_mapper));

    private IInStockService? _inStocks;
    public IInStockService InStocks =>
        _inStocks ??= new InStockService(UnitOfWork.InStocks, new InStockMapper(_mapper));

    private ILocationService? _locations;
    public ILocationService Locations =>
        _locations ??= new LocationService(UnitOfWork.Locations, new LocationMapper(_mapper));

    private IOrderService? _orders;
    public IOrderService Orders =>
        _orders ??= new OrderService(UnitOfWork.Orders, new OrderMapper(_mapper));

    private IPaymentTypeService? _paymentTypes;
    public IPaymentTypeService PaymentTypes =>
        _paymentTypes ??= new PaymentTypeService(UnitOfWork.PaymentTypes, new PaymentTypeMapper(_mapper));

    private IProductService? _products;
    public IProductService Products =>
        _products ??= new ProductService(UnitOfWork.Products, new ProductMapper(_mapper));

    private ISellerService? _sellers;
    public ISellerService Sellers =>
        _sellers ??= new SellerService(UnitOfWork.Sellers, new SellerMapper(_mapper));

    private IShippingInfoService? _shippingInfos;
    public IShippingInfoService ShippingInfos =>
        _shippingInfos ??= new ShippingInfoService(UnitOfWork.ShippingInfos, new ShippingInfoMapper(_mapper));

    private IShippingInfoAppUserService? _shippingInfoAppUsers;
    public IShippingInfoAppUserService ShippingInfoAppUsers =>
        _shippingInfoAppUsers ??= new ShippingInfoAppUserService(UnitOfWork.ShippingInfoAppUsers, new ShippingInfoAppUserMapper(_mapper));

    private IShippingInfoCustomerService? _shippingInfoCustomers;
    public IShippingInfoCustomerService ShippingInfoCustomers =>
        _shippingInfoCustomers ??= new ShippingInfoCustomerService(UnitOfWork.ShippingInfoCustomers, new ShippingInfoCustomerMapper(_mapper));

    private ISpecificationService? _specifications;
    public ISpecificationService Specifications =>
        _specifications ??= new SpecificationService(UnitOfWork.Specifications, new SpecificationMapper(_mapper));

    private ISpecificationTypeService? _specificationTypes;
    public ISpecificationTypeService SpecificationTypes =>
        _specificationTypes ??= new SpecificationTypeService(UnitOfWork.SpecificationTypes, new SpecificationTypeMapper(_mapper));

    private ITransactionReportService? _transactionReports;
    public ITransactionReportService TransactionReports =>
        _transactionReports ??= new TransactionReportService(UnitOfWork.TransactionReports, new TransactionReportMapper(_mapper));    
    
    private IPictureService? _pictures;
    public IPictureService Pictures =>
        _pictures ??= new PictureService(UnitOfWork.Pictures, new PictureMapper(_mapper));

    private IAppUserService? _appUsers;

    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUser, new AppUserMapper(_mapper));

    private IRefreshTokenService? _refreshTokens;

    public IRefreshTokenService RefreshTokens =>
        _refreshTokens ??= new RefreshTokenService(UnitOfWork.RefreshTokens, new RefreshTokenMapper(_mapper));

    private IProductOrdersService? _productOrders;

    public IProductOrdersService ProductOrders =>
        _productOrders ??= new ProductOrdersService(UnitOfWork.ProductOrders, new ProductOrdersMapper(_mapper));

    private IInvoiceService? _invoice;

    public IInvoiceService Invoice =>
        _invoice ??= new InvoiceService(UnitOfWork.Invoice, new InvoiceMapper(_mapper));
}