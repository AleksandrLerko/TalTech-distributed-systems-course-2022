using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ShippingInfo
{

    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(256)]
    public string AddressOne { get; set; } = default!;
    
    [MaxLength(256)]
    public string AddressTwo { get; set; } = default!;

    public Customer? Customer { get; set; }
    public ICollection<ShippingInfoAppUser>? ShippingInfoAppUsers { get; set; }
    
    public ICollection<Order>? Orders { get; set; }
}