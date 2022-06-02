using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class ShippingInfoAppUser : DomainEntityMetaId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }

}