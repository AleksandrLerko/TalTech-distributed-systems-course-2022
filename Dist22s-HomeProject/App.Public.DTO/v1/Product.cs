using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Product : DomainEntityId
{
    [MaxLength(256)]
    public string ProductName { get; set; } = default!;
    
    [MaxLength(2048)]
    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    // public Category? Category { get; set; }
    
    public Guid CurrencyId { get; set; }
    // public Currency? Currency { get; set; }
    
    public Guid SellerId { get; set; }
    // public Seller? Seller { get; set; }

    public Picture? Picture { get; set; }
    public InStock? InStocks { get; set; }
    public ICollection<Specification>? Specifications { get; set; }
    
    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<ProductOrders>? ProductOrders { get; set; }
}