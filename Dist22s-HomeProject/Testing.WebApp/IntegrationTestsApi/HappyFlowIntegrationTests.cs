using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Testing.WebApp.IntegrationTestsApi;

public class HappyFlowIntegrationTests
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public HappyFlowIntegrationTests(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }


    [Fact]
    public async Task Get_ShippingInfoAppUser_Api_Returns_Unauthorized()
    {
        var response = await _client.GetAsync("api/v1.0/ShippingInfoAppUsers");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}