using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using App.DAL.EF.Repositories.Identity;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    protected readonly AutoMapper.IMapper _mapper;
    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private ICategoryRepository? _categories;
    
    
    public virtual ICategoryRepository Categories =>
        _categories ??= new CategoryRepository(UOWDbContext, new CategoryMapper(_mapper));

    private ICurrencyRepository? _currencies;

    public virtual ICurrencyRepository Currencies =>
        _currencies ??= new CurrencyRepository(UOWDbContext, new CurrencyMapper(_mapper));

    private ISellerRepository? _sellers;

    public virtual ISellerRepository Sellers =>
        _sellers ??= new SellerRepository(UOWDbContext, new SellerMapper(_mapper));

    private ICategoryTypeRepository? _categoryTypes;

    public virtual ICategoryTypeRepository CategoryTypes =>
        _categoryTypes ??= new CategoryTypeRepository(UOWDbContext, new CategoryTypeMapper(_mapper));

    private IProductRepository? _products;

    public virtual IProductRepository Products =>
        _products ??= new ProductRepository(UOWDbContext, new ProductMapper(_mapper));

     private IOrderRepository? _orders; 
     public virtual IOrderRepository Orders =>
         _orders ??= new OrderRepository(UOWDbContext, new OrderMapper(_mapper));
     
     private IFeedbackRepository? _feedbacks; 
     public virtual IFeedbackRepository Feedbacks =>
         _feedbacks ??= new FeedbackRepository(UOWDbContext, new FeedbackMapper(_mapper));
     
     private IInStockRepository? _inStocks; 
     public virtual IInStockRepository InStocks =>
         _inStocks ??= new InStockRepository(UOWDbContext, new InStockMapper(_mapper));    
     
     private ILocationRepository? _locations; 
     public virtual ILocationRepository Locations =>
         _locations ??= new LocationRepository(UOWDbContext, new LocationMapper(_mapper));    
    
    private IPaymentTypeRepository? _paymentTypes; 
    public virtual IPaymentTypeRepository PaymentTypes =>
        _paymentTypes ??= new PaymentTypeRepository(UOWDbContext, new PaymentTypeMapper(_mapper));    
    
    private IShippingInfoRepository? _shippingInfos; 
    public virtual IShippingInfoRepository ShippingInfos =>
        _shippingInfos ??= new ShippingInfoRepository(UOWDbContext, new ShippingInfoMapper(_mapper));    
    
    private IShippingInfoAppUserRepository? _shippingInfoAppUsers; 
    public virtual IShippingInfoAppUserRepository ShippingInfoAppUsers =>
        _shippingInfoAppUsers ??= new ShippingInfoAppUserRepository(UOWDbContext, new ShippingInfoAppUserMapper(_mapper));    
    
    private IShippingInfoCustomerRepository? _shippingInfoCustomers; 
    public virtual IShippingInfoCustomerRepository ShippingInfoCustomers =>
        _shippingInfoCustomers ??= new ShippingInfoCustomerRepository(UOWDbContext, new ShippingInfoCustomerMapper(_mapper));    
    
    private ISpecificationRepository? _specifications; 
    public virtual ISpecificationRepository Specifications =>
        _specifications ??= new SpecificationRepository(UOWDbContext, new SpecificationMapper(_mapper));    
    
    private ISpecificationTypeRepository? _specificationTypes; 
    public virtual ISpecificationTypeRepository SpecificationTypes =>
        _specificationTypes ??= new SpecificationTypeRepository(UOWDbContext, new SpecificationTypeMapper(_mapper)); 
    
    private ICustomerRepository? _customers;
    public virtual ICustomerRepository Customers =>
        _customers ??= new CustomerRepository(UOWDbContext, new CustomerMapper(_mapper));    
    private ITransactionReportRepository? _transactionReports;
    public virtual ITransactionReportRepository TransactionReports =>
        _transactionReports ??= new TransactionReportRepository(UOWDbContext, new TransactionReportMapper(_mapper));   
    
    private IDeliveryTypeRepository? _deliveryTypes;
    public virtual IDeliveryTypeRepository DeliveryTypes =>
        _deliveryTypes ??= new DeliveryTypeRepository(UOWDbContext, new DeliveryTypeMapper(_mapper));

    private IPictureRepository? _pictures;

    public virtual IPictureRepository Pictures =>
        _pictures ??= new PictureRepository(UOWDbContext, new PictureMapper(_mapper));

    private IAppUserRepository? _appUser;
    
    public virtual IAppUserRepository AppUser =>
        _appUser ??= new AppUserRepository(UOWDbContext, new AppUserMapper(_mapper));

    private IRefreshTokenRepository? _refreshTokens;

    public virtual IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(UOWDbContext, new RefreshTokenMapper(_mapper));

    private IProductOrdersRepository? _productOrders;

    public virtual IProductOrdersRepository ProductOrders =>
        _productOrders ??= new ProductOrdersRepository(UOWDbContext, new ProductOrdersMapper(_mapper));

    private IInvoiceRepository? _invoice;

    public virtual IInvoiceRepository Invoice =>
        _invoice ??= new InvoiceRepository(UOWDbContext, new InvoiceMapper(_mapper));

}