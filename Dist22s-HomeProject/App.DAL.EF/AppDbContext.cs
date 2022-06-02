using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<DeliveryType> DeliveryTypes { get; set; } = default!;
    public DbSet<Feedback> Feedbacks { get; set; } = default!;
    public DbSet<InStock> InStocks { get; set; } = default!;
    public DbSet<Location> Locations { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Seller> Sellers { get; set; } = default!;
    public DbSet<ShippingInfo> ShippingInfos { get; set; } = default!;
    public DbSet<ShippingInfoAppUser> ShippingInfoAppUsers { get; set; } = default!;
    public DbSet<ShippingInfoCustomer> ShippingInfoCustomers { get; set; } = default!;
    public DbSet<Specification> Specifications { get; set; } = default!;
    public DbSet<SpecificationType> SpecificationTypes { get; set; } = default!;
    public DbSet<TransactionReport> TransactionReports { get; set; } = default!;
    public DbSet<CategoryType> CategoryTypes { get; set; } = default!;
    public DbSet<Picture> Pictures { get; set; } = default!;

    public DbSet<ProductOrders> ProductOrders { get; set; } = default!;
    public DbSet<Invoice> Invoice { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // builder.Entity<LangStr>().HasNoKey();
        base.OnModelCreating(builder);
        // Remove cascade delete
        
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            builder
                .Entity<DeliveryType>()
                .Property(e => e.TypeName)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            
            builder
                .Entity<PaymentType>()
                .Property(e => e.TypeName)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));     
            
            builder
                .Entity<Product>()
                .Property(e => e.ProductName)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));   
            
            builder
                .Entity<Product>()
                .Property(e => e.Description)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
        }
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
    }
    
    private static string SerialiseLangStr(LangStr lStr) => System.Text.Json.JsonSerializer.Serialize(lStr);

    private static LangStr DeserializeLangStr(string jsonStr) =>
        System.Text.Json.JsonSerializer.Deserialize<LangStr>(jsonStr) ?? new LangStr();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }

    
    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }

}