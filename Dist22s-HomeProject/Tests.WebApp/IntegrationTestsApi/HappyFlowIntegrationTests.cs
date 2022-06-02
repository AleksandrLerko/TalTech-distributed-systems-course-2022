using System.Net;
using System.Net.Http.Headers;
using System.Text;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.WebApp.Helpers;
using Xunit.Abstractions;

namespace Tests.WebApp.IntegrationTestsApi;

public class HappyFlowIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
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


    // [Fact]
    // public async Task Get_ShippingInfoAppUser_Api_Returns_Unauthorized()
    // {
    //     var response = await _client.GetAsync("api/v1.0/ShippingInfoAppUsers");
    //     
    //     Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    // }

    [Fact]
    public async void StartApp()
    {
        await Register_Api();
    }

    public async Task Register_Api()
    {
        // ARRANGE
        var uri = "/api/v1.0/Identity/Account/Register";

        var registerDto = new Register()
        {
            Email = "allerk@ttu.ee",
            FirstName = "Aleksandr",
            LastName = "Lerko",
            Password = "FooBar1!"
        };
        
        var jsonStr = System.Text.Json.JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        
        // ACT
        var getRegister = await _client.PostAsync(uri, data);
        
        // ASSERT

        var content = await getRegister.Content.ReadAsStringAsync();
        
        var actualData = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(content);
        
        Assert.Equal(200, (int) getRegister.StatusCode);
        Assert.Equal("Aleksandr", actualData!.FirstName);
        Assert.Equal("Lerko", actualData!.LastName);
        _testOutputHelper.WriteLine("Register success!");
        await Login_Api();
    }

    public async Task Login_Api()
    {
        // ARRANGE
        var uri = "/api/v1.0/Identity/Account/Login";

        var loginDto = new Login()
        {
            Email = "allerk@ttu.ee",
            Password = "FooBar1!"
        };
        
        var dataToPost = JsonHelper.GetStringContent(loginDto);
        
        // ACT
        var getResponse = await _client.PostAsync(uri, dataToPost);
        
        // ASSERT
        getResponse.EnsureSuccessStatusCode();
        Assert.Equal(200, (int) getResponse.StatusCode);
        
        var body = await getResponse.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
        
        Assert.NotNull(data);
        Assert.Equal("Aleksandr", data!.FirstName);
        Assert.Equal("Lerko", data!.LastName);
        _testOutputHelper.WriteLine("Login success!");
        await Create_Order_And_ShippingInfoAppUser(data.Token, data.AppUserId, data.FirstName, data.LastName, data.Email);
    }

    public async Task Create_Order_And_ShippingInfoAppUser(string token, Guid appUserId, string firstName, string lastName, string email)
    {
        // ARRANGE
        var uri_postOrder = "/api/v1.0/Orders";
        var uri_getDeliveryType = "/api/v1.0/DeliveryTypes";
        var uri_getShippingInfo = "/api/v1.0/ShippingInfos";
        var uri_getPaymentType = "/api/v1.0/PaymentTypes";
        var uri_shippingInfoAppUsers = "/api/v1.0/shippinginfoappusers";

        /* Get related to orders objects data */
        var requestDeliveryType = await _client.GetAsync(uri_getDeliveryType);
        var requestShippingInfo = await _client.GetAsync(uri_getShippingInfo);
        var requestPaymentType = await _client.GetAsync(uri_getPaymentType);
        
        requestDeliveryType.EnsureSuccessStatusCode();
        requestShippingInfo.EnsureSuccessStatusCode();
        requestPaymentType.EnsureSuccessStatusCode();
        
        var bodyDeliveryType = await requestDeliveryType.Content.ReadAsStringAsync();
        var bodyShippingInfo = await requestShippingInfo.Content.ReadAsStringAsync();
        var bodyPaymentType = await requestPaymentType.Content.ReadAsStringAsync();

        var deliveryTypeList = JsonHelper.DeserializeWithWebDefaults<ICollection<DeliveryType>>(bodyDeliveryType);
        var shippingInfoList = JsonHelper.DeserializeWithWebDefaults<ICollection<ShippingInfo>>(bodyShippingInfo);
        var paymentTypeList = JsonHelper.DeserializeWithWebDefaults<ICollection<PaymentType>>(bodyPaymentType);

        var deliveryType = deliveryTypeList!.FirstOrDefault(x => x.TypeName == "Omniva");
        var shippingInfo = shippingInfoList!.FirstOrDefault(x => x.AddressOne == "Akadeemia tee 11" && x.AddressTwo == "4");
        var paymentType = paymentTypeList!.FirstOrDefault(x => x.TypeName == "Card");
        
        // Create Order
        var orderDto = new Order()
        {
            AppUserId = appUserId,
            DeliveryTypeId = deliveryType!.Id,
            PaymentTypeId = paymentType!.Id,
            ShippingInfoId = shippingInfo!.Id
        };
        
        var orderToPost = JsonHelper.GetStringContent(orderDto);
        
        // Create ShippingInfoAppUser
        var shippingInfoAppUserDto = new ShippingInfoAppUser()
        {
            AppUserId = appUserId,
            ShippingInfoId = shippingInfo.Id
        };
        
        var shippingInfoAppUserToPost = JsonHelper.GetStringContent(shippingInfoAppUserDto);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        // ACT
        var getResponseOrder = await _client.PostAsync(uri_postOrder, orderToPost);
        var getResponseShippingInfoAppUser = await _client.PostAsync(uri_shippingInfoAppUsers, shippingInfoAppUserToPost);
        
        
        // ASSERT
        getResponseOrder.EnsureSuccessStatusCode();
        getResponseShippingInfoAppUser.EnsureSuccessStatusCode();
        Assert.Equal(201, (int) getResponseOrder.StatusCode);
        Assert.Equal(201, (int) getResponseShippingInfoAppUser.StatusCode);
        
        var body = await getResponseOrder.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<Order>(body);
        
        Assert.NotNull(data);
        _testOutputHelper.WriteLine("Order and ShippingInfoAppUser successfully created!");
        
        await Purchase_Api(
            data!.Id,
            firstName,
            lastName,
            email,
            paymentType.TypeName,
            deliveryType.TypeName,
            shippingInfo.AddressOne + ", " + shippingInfo.AddressTwo,
            deliveryType.Price)
            ;
    }
    public async Task Purchase_Api(
        Guid orderId,
        string firstName,
        string lastName,
        string email,
        string paymentName,
        string deliveryName,
        string fullAddress,
        int deliveryPrice)
    {
        // ARRANGE
        var uri_product = "/api/v1.0/Products";
        var uri_inStocks = "/api/v1.0/InStocks";
        var uri_transactionReport = "/api/v1.0/TransactionReports";
        var uri_invoice = "/api/v1.0/Invoices";
        var uri_productOrders = $"/api/v1.0/ProductOrders";
        
        /* Get product data */
        var requestProduct = await _client.GetAsync(uri_product);
        var requestStocks = await _client.GetAsync(uri_inStocks);
        
        requestProduct.EnsureSuccessStatusCode();
        requestStocks.EnsureSuccessStatusCode();
        
        var bodyProduct = await requestProduct.Content.ReadAsStringAsync();
        var bodyStocks = await requestStocks.Content.ReadAsStringAsync();

        var productsList = JsonHelper.DeserializeWithWebDefaults<ICollection<Product>>(bodyProduct);
        var stocksList = JsonHelper.DeserializeWithWebDefaults<ICollection<InStock>>(bodyStocks);

        var product = productsList!.FirstOrDefault(x =>
            x.ProductName == "Macbook" &&
            x.Description == "Powerful" &&
            x.Price == 2200
            );

        var stocks = stocksList!.FirstOrDefault(x => x.ProductId == product!.Id);

        var numToBuy = 0;
        if (productsList!.Count <= stocks!.Quantity)
        {
            numToBuy = stocks.Quantity - (stocks.Quantity - productsList.Count);
            stocks.Quantity -= productsList.Count;
        }
        else
        {
            throw new Exception(
                $"Products are not enough for your purchase, products in stock ${stocks!.Quantity}, but you want to buy ${productsList.Count}!");
        }

        var invoice = new Invoice()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow.ToString(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PaymentMethodName = paymentName,
            DeliveryMethodName = deliveryName,
            FullAddress = fullAddress,
            FinalPrice = (int) product!.Price * numToBuy + deliveryPrice
        };
        
        var transactionReport = new TransactionReport()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            TotalPrice = (int) product!.Price * numToBuy + deliveryPrice,
            InvoiceId = invoice.Id
        };

        var productOrderDto = new ProductOrders()
        {
            OrderId = orderId,
            ProductId = product!.Id,
            TransactionReportId = transactionReport.Id
        };
        
        
        var productOrdersToPost = JsonHelper.GetStringContent(productOrderDto);
        var stocksToUpdate = JsonHelper.GetStringContent(stocks);
        var transactionReportToPost = JsonHelper.GetStringContent(transactionReport);
        var invoiceToPost = JsonHelper.GetStringContent(invoice);
        
        var uri_inStocksPut = $"/api/v1.0/InStocks/{stocks.Id}";
        
        // ACT
        var getResponseProductOrders = await _client.PostAsync(uri_productOrders, productOrdersToPost);
        var getResponseStocks = await _client.PutAsync(uri_inStocksPut, stocksToUpdate);
        var getResponseReport = await _client.PostAsync(uri_transactionReport, transactionReportToPost);
        var getResponseInvoice = await _client.PostAsync(uri_invoice, invoiceToPost);
        
        // ASSERT
        
        getResponseProductOrders.EnsureSuccessStatusCode();
        getResponseStocks.EnsureSuccessStatusCode();
        getResponseReport.EnsureSuccessStatusCode();
        getResponseInvoice.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine("Calculations have been made and invoice was created!");
        await Get_Invoice_Api();
    }

    public async Task Get_Invoice_Api()
    {
        // ARRANGE
        var uri = "/api/v1.0/Invoices";
        
        // ACT
        var invoiceProduct = await _client.GetAsync(uri);
        
        // ASSERT
        invoiceProduct.EnsureSuccessStatusCode();
        var bodyInvoice = await invoiceProduct.Content.ReadAsStringAsync();

        var invoiceList = JsonHelper.DeserializeWithWebDefaults<ICollection<Invoice>>(bodyInvoice);

        var invoice = invoiceList!.FirstOrDefault();
        
        Assert.NotNull(invoice);
        Assert.Equal("Aleksandr Lerko", invoice!.FirstName + " " + invoice.LastName);
        Assert.Equal("Akadeemia tee 11, 4", invoice.FullAddress);
        Assert.Equal("allerk@ttu.ee", invoice.Email);
        Assert.Equal(2202, invoice.FinalPrice);
        Assert.Equal("Card", invoice.PaymentMethodName);
        Assert.Equal("Omniva", invoice.DeliveryMethodName);
        
        _testOutputHelper.WriteLine("Invoice was done correctly, purchase is successed!");
    }
}