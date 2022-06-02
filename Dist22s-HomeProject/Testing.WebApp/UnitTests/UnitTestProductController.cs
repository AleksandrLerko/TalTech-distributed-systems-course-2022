using App.BLL;
using App.BLL.DTO;
using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.Contracts.Base;
using Base.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp.ApiControllers;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace Testing.WebApp.UnitTests;

public class UnitTestProductController
{
    // private readonly TestController _testController;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _ctx;

    // TunitofWork - AppUnit of work
    // BaseEntityService<IAppUnitOfWork, IBillRepository, BLLAppDTO.Bill, DALAppDTO.Bill>
    private readonly Mock<IProductRepository> _mockProductRepository;
    // private readonly Mock<IMapper> _mockedMapper;

    private readonly IMapper _DALBLLMapper;
    private readonly IMapper _DALDomainMapper;
    private readonly ProductService _productService;

    // private readonly Mock<IAppUnitOfWork> _uow;
    private readonly AppUOW _uow;
    
    private readonly Mock<IAppBLL> _bllMock;
    
    private readonly ProductsController _productsController;

    public UnitTestProductController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // configuring IMapper manually, Dependency Injection cannot reach here
        var myProfile = new App.BLL.AutomapperConfig();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);

        var myProfile1 = new App.DAL.EF.AutomapperConfig();
        var configuration1 = new MapperConfiguration(cfg => cfg.AddProfile(myProfile1));
            
        _DALDomainMapper = new Mapper(configuration1);

        _DALBLLMapper = mapper;
        //
        _mockProductRepository = new Mock<IProductRepository>();

        // set up mock db - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        _uow = new AppUOW(_ctx, _DALDomainMapper);

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<ProductsController>();

        _bllMock = new Mock<IAppBLL>();
        
        // SUT
        
        _mockProductRepository.Setup(x =>
            x.Add(It.Is<App.DAL.DTO.Product>(p => p.GetType() == typeof(App.DAL.DTO.Product)))).Returns(new 
            App.DAL.DTO.Product()
        {
            ProductName = new LangStr("ProductAdd"),
            Description = new LangStr("ProductAdd"),
            Price = 10
        });        
        
        _mockProductRepository.Setup(x =>
            x.Update(It.Is<App.DAL.DTO.Product>(p => p.ProductName == "ProductOld"))).Returns(new 
            App.DAL.DTO.Product()
        {
            ProductName = new LangStr("ProductUpdated"),
            Description = new LangStr("ProductUpdated"),
            Price = 10
        });
        
        _mockProductRepository.Setup(x =>
            x.Remove(
                It.Is<App.DAL.DTO.Product>(p => p.ProductName == "ProductRemoveEntity"))).Returns(new App.DAL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveEntity"),
            Description = new LangStr("ProductRemoveEntity"),
            Price = 10
        });        
        
        _mockProductRepository.Setup(x =>
            x.Remove(
                It.Is<Guid>(p => p == Guid.Empty))).Returns(new App.DAL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveKey"),
            Description = new LangStr("ProductRemoveKey"),
            Price = 10
        });        
        
        _mockProductRepository.Setup(x =>
            x.RemoveAsync(
                It.Is<Guid>(p => p == Guid.Empty))).ReturnsAsync(new App.DAL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveAsync"),
            Description = new LangStr("ProductRemoveAsync"),
            Price = 10
        });
        
        _mockProductRepository.Setup(x => x.GetAllAsync(true)).ReturnsAsync(new List<App.DAL.DTO.Product>()
        {
            new()
            {
                ProductName = new LangStr("TEST"),
                Description = new LangStr("TEST"),
                Price = 10
            },            
            new()
            {
                ProductName = new LangStr("TEST2"),
                Description = new LangStr("TEST2"),
                Price = 20
            }
        });         
        
        _mockProductRepository.Setup(x => x.GetAll(true)).Returns(new List<App.DAL.DTO.Product>()
        {
            new()
            {
                ProductName = new LangStr("GetAll"),
                Description = new LangStr("GetAll"),
                Price = 10
            },            
            new()
            {
                ProductName = new LangStr("GetAll2"),
                Description = new LangStr("GetAll2"),
                Price = 20
            }
        });        
        
        _mockProductRepository.Setup(x => x.FirstOrDefaultAsync(
            It.Is<Guid>(a => a == Guid.Empty),
            It.Is<bool>(a => a == true))).ReturnsAsync(new App.DAL.DTO.Product()
        {
            ProductName = new LangStr("TestFirstOrDefaultAsync"),
                Description = new LangStr("TestFirstOrDefaultAsync"),
                Price = 10
        });        
        
        _mockProductRepository.Setup(x => x.FirstOrDefault(
            It.Is<Guid>(a => a == Guid.Empty),
            It.Is<bool>(a => a == true))).Returns(new App.DAL.DTO.Product()
        {
            ProductName = new LangStr("TestFirstOrDefault"),
            Description = new LangStr("TestFirstOrDefault"),
            Price = 10
        });
        
        _mockProductRepository.Setup(x =>
            x.ExistsAsync(
                It.Is<Guid>(a => a == Guid.Empty))).ReturnsAsync(true);        
        
        _mockProductRepository.Setup(x =>
            x.Exists(
                It.Is<Guid>(a => a == Guid.Empty))).Returns(true);
        
        
        // custom methods
        
        _mockProductRepository.Setup(x =>
            x.GetProductByName(
                It.Is<string>(a => a.Equals("TestByName")))).ReturnsAsync(
            new List<App.DAL.DTO.Product>()
            {
                new App.DAL.DTO.Product()
                {
                    ProductName = new LangStr("TestByName"),
                    Description = new LangStr("TestByName"),
                    Price = 20
                }
                
            });        
        
        _mockProductRepository.Setup(x =>
            x.GetProductsByCategory(
                It.Is<Guid>(a => a == Guid.Empty))).ReturnsAsync(
            new List<App.DAL.DTO.Product>()
            {
                new App.DAL.DTO.Product()
                {
                    ProductName = new LangStr("TestByCategoryId"),
                    Description = new LangStr("TestByCategoryId"),
                    Price = 20
                }
                
            });
        
        _productService = new ProductService(
            _mockProductRepository.Object,
            new App.BLL.Mappers.ProductMapper(_DALBLLMapper)
        );

        _productsController = new ProductsController(_bllMock.Object, logger);

    }
    
    [Fact]
    public void Test_Products_GetAllAsync()
    {

        var result = _productService.GetAllAsync(true).Result;

        List<App.BLL.DTO.Product> listOfProducts = new()
        {
            new Product
            {
                ProductName = new LangStr("TEST"),
                Description = new LangStr("TEST"),
                Price = 10
            },            
            new Product
            {
                ProductName = new LangStr("TEST2"),
                Description = new LangStr("TEST2"),
                Price = 20
            }
        };

        var expected = listOfProducts.AsEnumerable();
            
        Assert.NotNull(result);
        Assert.NotStrictEqual(expected, result);

        _mockProductRepository.Verify(x => x.GetAllAsync(
            It.Is<bool>(a => a == true)
        ), Times.Once);
    }    
    
    [Fact]
    public void Test_Products_GetAll()
    {

        var result = _productService.GetAll(true);

        List<App.BLL.DTO.Product> listOfProducts = new()
        {
            new Product
            {
                ProductName = new LangStr("GetAll"),
                Description = new LangStr("GetAll"),
                Price = 10
            },            
            new Product
            {
                ProductName = new LangStr("GetAll2"),
                Description = new LangStr("GetAll2"),
                Price = 20
            }
        };

        var expected = listOfProducts.AsEnumerable();
            
        Assert.NotNull(result);
        Assert.NotStrictEqual(expected, result);

        _mockProductRepository.Verify(x => x.GetAll(
            It.Is<bool>(a => a == true)
        ), Times.Once);
    }
    
    [Fact]
    public void Test_Products_FirstOrDefaultAsync()
    {

        var result = _productService.FirstOrDefaultAsync(Guid.Empty).Result;

        var product = new App.BLL.DTO.Product()
        {
            ProductName = new LangStr("TestFirstOrDefaultAsync"),
            Description = new LangStr("TestFirstOrDefaultAsync"),
            Price = 10
        };

        var expected = product;
            
        Assert.NotNull(result);
        Assert.Equal(result!.ProductName, expected.ProductName);
        Assert.Equal(result!.Description, expected.Description);
        Assert.Equal(result!.Price, expected.Price);

        _mockProductRepository.Verify(x => x.FirstOrDefaultAsync(
            It.Is<Guid>(a => a.GetType() == typeof(Guid)),
            It.Is<bool>(a => a == true)
        ), Times.Once);
    }    
    
    [Fact]
    public void Test_Products_FirstOrDefault()
    {

        var result = _productService.FirstOrDefault(Guid.Empty);

        var product = new App.BLL.DTO.Product()
        {
            ProductName = new LangStr("TestFirstOrDefault"),
            Description = new LangStr("TestFirstOrDefault"),
            Price = 10
        };

        var expected = product;
            
        Assert.NotNull(result);
        Assert.Equal(result!.ProductName, expected.ProductName);
        Assert.Equal(result!.Description, expected.Description);
        Assert.Equal(result!.Price, expected.Price);

        _mockProductRepository.Verify(x => x.FirstOrDefault(
            It.Is<Guid>(a => a.GetType() == typeof(Guid)),
            It.Is<bool>(a => a == true)
        ), Times.Once);
    }
    
    [Fact]
    public void Test_Products_GetProductByName()
    {

        var result = _productService.GetProductByName("TestByName").Result;

        List<App.BLL.DTO.Product> listOfProducts = new()
        {
            new App.BLL.DTO.Product()
            {
                ProductName = new LangStr("TestByName"),
                Description = new LangStr("TestByName"),
                Price = 20
            }
        };

        var expected = listOfProducts.AsEnumerable();
            
        Assert.NotNull(result);
        Assert.NotStrictEqual(expected, result);

        _mockProductRepository.Verify(x => x.GetProductByName(
            It.Is<string>(a => a.Equals("TestByName"))
        ), Times.Once);
    }    
    
    [Fact]
    public void Test_Products_GetProductsByCategory()
    {

        var result = _productService.GetProductsByCategory(Guid.Empty).Result;

        List<App.BLL.DTO.Product> listOfProducts = new()
        {
            new App.BLL.DTO.Product()
            {
                ProductName = new LangStr("TestByCategoryId"),
                Description = new LangStr("TestByCategoryId"),
                Price = 20
            }
        };

        var expected = listOfProducts.AsEnumerable();
            
        Assert.NotNull(result);
        Assert.NotStrictEqual(expected, result);

        _mockProductRepository.Verify(x => x.GetProductsByCategory(
            It.Is<Guid>(a => a == Guid.Empty)
        ), Times.Once);
    }
    
    [Fact]
    public void Test_Products_Add()
    {
    
        var result = _productService.Add(new Product()
        {
            ProductName = new LangStr("ProductAdd"),
            Description = new LangStr("ProductAdd"),
            Price = 10
        });
    
        Assert.NotNull(result);
        Assert.True(result!.ProductName == "ProductAdd");
        Assert.True(result!.Description == "ProductAdd");
        Assert.True(result!.Price == 10);
    }    
    
    [Fact]
    public void Test_Products_Update()
    {
    
        var product = new Product()
        {
            ProductName = new LangStr("ProductOld"),
            Description = new LangStr("ProductOld"),
            Price = 10
        };

        var result = _productService.Update(product);
        Assert.NotNull(result);
        Assert.True(result!.ProductName == "ProductUpdated");
        Assert.True(result!.Description == "ProductUpdated");
        Assert.True(result!.Price == 10);
    }
    
    [Fact]
    public void Test_Product_RemoveByEntity()
    {
        var productRemove = new App.BLL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveEntity"),
            Description = new LangStr("ProductRemoveEntity"),
            Price = 10
        };
        
        var result = _productService.Remove(productRemove);
    
        var expected = productRemove;
            
        Assert.NotNull(result);
        Assert.Equal(expected.ProductName, result.ProductName);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.Price, result.Price);
            
        _mockProductRepository.Verify(x => x.Remove(
            It.Is<App.DAL.DTO.Product>(a => a.GetType() == typeof(App.DAL.DTO.Product))), Times.Once);
    }    
    
    [Fact]
    public void Test_Product_RemoveByKey()
    {
        var productRemove = new App.BLL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveKey"),
            Description = new LangStr("ProductRemoveKey"),
            Price = 10
        };
        
        var result = _productService.Remove(productRemove.Id);
    
        var expected = productRemove;
            
        Assert.NotNull(result);
        Assert.Equal(expected.ProductName, result.ProductName);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.Price, result.Price);
            
        _mockProductRepository.Verify(x => x.Remove(
            It.Is<Guid>(a => a == Guid.Empty)), Times.Once);
    }    
    
    [Fact]
    public void Test_Product_RemoveAsync()
    {
        var productRemove = new App.BLL.DTO.Product()
        {
            ProductName = new LangStr("ProductRemoveAsync"),
            Description = new LangStr("ProductRemoveAsync"),
            Price = 10
        };
        
        var result = _productService.RemoveAsync(productRemove.Id).Result!;
    
        var expected = productRemove;
            
        Assert.NotNull(result);
        Assert.Equal(expected.ProductName, result.ProductName);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.Price, result.Price);
            
        _mockProductRepository.Verify(x => x.RemoveAsync(
            It.Is<Guid>(a => a == Guid.Empty)), Times.Once);
    }
    
    [Fact]
    public void Test_Artist_ExistsAsync()
    {
        var result = _productService.ExistsAsync(Guid.Empty).Result;
        Assert.True(result);
    }    
    
    [Fact]
    public void Test_Artist_Exists()
    {
        var result = _productService.Exists(Guid.Empty);
        Assert.True(result);
    }
    
    

}