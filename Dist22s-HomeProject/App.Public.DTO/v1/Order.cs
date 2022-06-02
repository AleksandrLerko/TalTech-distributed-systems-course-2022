using Base.Domain;

namespace App.Public.DTO.v1;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CustomerId { get; set; }
    // public Customer? Customer { get; set; }

    public Guid? AppUserId { get; set; }
    
    public Guid DeliveryTypeId { get; set; }
    // public DeliveryType? DeliveryType { get; set; }

    // public Guid ProductId { get; set; }
    // public Product? Product { get; set; }

    public Guid PaymentTypeId { get; set; }

    public Guid ShippingInfoId { get; set; }
    // public PaymentType? PaymentType { get; set; }
    public TransactionReport? TransactionReport { get; set; }
    public ICollection<ProductOrders>? ProductOrders { get; set; }
}