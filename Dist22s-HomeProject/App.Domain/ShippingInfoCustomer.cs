using Base.Domain;

namespace App.Domain;

public class ShippingInfoCustomer : DomainEntityMetaId
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }
    
}