using System.ComponentModel.DataAnnotations;
using Base.Domain;


namespace App.DAL.DTO;

public class ShippingInfo : DomainEntityId
{
    [MaxLength(256)]
    //[Display(ResourceType = typeof(App.Recources.App.Domain.ShippingInfo), Name = nameof(AddressOne))]
    public string AddressOne { get; set; } = default!;
    
    [MaxLength(256)]
    //[Display(ResourceType = typeof(App.Recources.App.Domain.ShippingInfo), Name = nameof(AddressTwo))]
    public string AddressTwo { get; set; } = default!;
    
    //TODO : scaffold rest controllers again
    public Customer? Customer { get; set; }
    
    public ICollection<ShippingInfoCustomer>? ShippingInfoCustomers { get; set; }
    public ICollection<App.DAL.DTO.ShippingInfoAppUser>? ShippingInfoAppUsers { get; set; }
    
    public ICollection<Order>? Orders { get; set; }
}