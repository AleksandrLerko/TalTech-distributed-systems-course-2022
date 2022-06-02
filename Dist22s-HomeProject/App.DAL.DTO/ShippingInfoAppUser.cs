using App.DAL.DTO.Identity;
using Base.Domain;


namespace App.DAL.DTO;

public class ShippingInfoAppUser : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }
}