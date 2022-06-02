using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Testing.WebApp;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private static bool dbInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // find DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));

            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // and new DbContext
            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });

            // data seeding
            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                if (dbInitialized == false)
                {
                    dbInitialized = true;
                    // DataSeeder.SeedData(db);
                    
                    // Seed data
                    if (db.DeliveryTypes.Any()) return;

                    var deliveryType = new DeliveryType()
                    {
                        TypeName = "Courier",
                        Price = 2
                    };
                    
                    db.DeliveryTypes.Add(deliveryType);

                    if (db.Currencies.Any()) return;
                    
                    var currency = new Currency()
                    {
                        CurrencyName = "Euro"
                    };
                    
                    db.Currencies.Add(currency);

                    if (db.Categories.Any()) return;
                    
                    var category = new Category()
                    {
                        CategoryName = "Phones"
                    };
                    
                    db.Categories.Add(category);

                    if (db.Sellers.Any()) return;
                    
                    var seller = new Seller()
                    {
                        SellerName = "Apple"
                    };
                    
                    db.Sellers.Add(seller);

                    if (db.Products.Any()) return;
                    
                    var product = new Product()
                    {
                        ProductName = "Macbook",
                        Description = "Powerful",
                        Price = 2200,
                        CategoryId = category.Id,
                        Category = null,
                        CurrencyId = currency.Id,
                        Currency = null,
                        SellerId = seller.Id,
                        Seller = null
                    };
                    
                    db.Products.Add(product);

                    if (db.InStocks.Any()) return;
                    
                    var stocks = new InStock()
                    {
                        Quantity = 4,
                        ProductId = product.Id
                    };

                    db.InStocks.Add(stocks);

                    if (db.Pictures.Any()) return;
                    
                    var picture = new Picture()
                    {
                        FilePath = "picturePng",
                        ProductId = product.Id
                    };

                    db.Pictures.Add(picture);
                    db.SaveChanges();

                    if (db.PaymentTypes.Any()) return;
                    
                    var paymentType = new PaymentType()
                    {
                        TypeName = "Card",
                        Comment = "Test"
                    };

                    db.PaymentTypes.Add(paymentType);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}