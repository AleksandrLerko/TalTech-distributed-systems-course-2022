using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Product : DomainEntityMetaId
{
    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Product), Name = nameof(ProductName))]
    public LangStr ProductName { get; set; } = new();

    [MaxLength(2048)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Product), Name = nameof(Description))]
    public LangStr Description { get; set; } = new();

    [Display(ResourceType = typeof(App.Recources.App.Domain.Product), Name = nameof(Price))]
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public Guid SellerId { get; set; }
    public Seller? Seller { get; set; }

    public Picture? Picture { get; set; }
    public InStock? InStocks { get; set; }
    public ICollection<Specification>? Specifications { get; set; }

    public ICollection<Feedback>? Feedbacks { get; set; }

    public ICollection<ProductOrders>? ProductOrders { get; set; }
    
}