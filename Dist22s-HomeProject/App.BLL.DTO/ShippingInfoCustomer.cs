using Base.Domain;

namespace App.BLL.DTO;

public class ShippingInfoCustomer : DomainEntityId
{
    public Guid CustomerId { get; set; }
    public App.BLL.DTO.Customer? Customer { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    public App.BLL.DTO.ShippingInfo? ShippingInfo { get; set; }
    
}