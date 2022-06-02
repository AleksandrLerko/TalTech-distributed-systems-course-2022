using System.ComponentModel.DataAnnotations;
using Base.Domain;


namespace App.DAL.DTO;

public class Customer : DomainEntityId
{
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Customer), Name = nameof(FirstName))]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Customer), Name = nameof(LastName))]
    public string LastName { get; set; } = default!;
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Customer), Name = nameof(Email))]
    public string Email { get; set; } = default!;

    [MaxLength(64)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Customer), Name = nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = default!;
    
    public Guid ShippingInfoId { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }
    
    public ICollection<ShippingInfoCustomer>? ShippingInfoCustomers { get; set; }
    public ICollection<App.DAL.DTO.Order>? Orders { get; set; }
}