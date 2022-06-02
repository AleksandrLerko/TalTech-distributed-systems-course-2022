using Base.Domain;

namespace App.DAL.DTO;

public class ShippingInfoCustomer : DomainEntityId
{
    public Guid CustomerId { get; set; }
    public App.DAL.DTO.Customer? Customer { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    public App.DAL.DTO.ShippingInfo? ShippingInfo { get; set; }
    
}