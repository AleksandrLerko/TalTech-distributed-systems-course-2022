using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
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
    // public ShippingInfo? ShippingInfo { get; set; }
    
    // public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<Order>? Orders { get; set; }
}