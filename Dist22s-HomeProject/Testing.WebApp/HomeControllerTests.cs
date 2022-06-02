using Microsoft.Extensions.Logging;
using WebApp.Controllers;
using Xunit;

namespace Testing.WebApp;

public class HomeControllerTests
{
    private readonly HomeController _homeController;
    
    // Arrange
    public HomeControllerTests()
    {
        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<HomeController>();
        _homeController = new HomeController(logger);
    }
    
    [Fact]
    public void Test1_TestAction()
    {
        // ARRANGE
        var a = 1;
        var b = 2;
        
        // ACT
        var result = _homeController.TestAction(a, b);
        
        // ASSERT
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.Equal(1, result.Length);
        Assert.Equal("3", result);
    }
}