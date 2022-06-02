using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class ShippingInfo : DomainEntityId
{
    [MaxLength(256)]
    //[Display(ResourceType = typeof(App.Recources.App.Domain.ShippingInfo), Name = nameof(AddressOne))]
    public string AddressOne { get; set; } = default!;
    
    [MaxLength(256)]
    //[Display(ResourceType = typeof(App.Recources.App.Domain.ShippingInfo), Name = nameof(AddressTwo))]
    public string AddressTwo { get; set; } = default!;

    public Customer? Customer { get; set; }
    
    public ICollection<App.BLL.DTO.ShippingInfoCustomer>? ShippingInfoCustomers { get; set; }
    public ICollection<App.BLL.DTO.ShippingInfoAppUser>? ShippingInfoAppUsers { get; set; }
    
    public ICollection<Order>? Orders { get; set; }
}