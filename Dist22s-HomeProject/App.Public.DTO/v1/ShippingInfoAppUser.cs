using Base.Domain;

namespace App.Public.DTO.v1;

public class ShippingInfoAppUser : DomainEntityId
{
    public Guid AppUserId { get; set; }
    // public Identity.AppUser? AppUser { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    // public ShippingInfo? ShippingInfo { get; set; }
}