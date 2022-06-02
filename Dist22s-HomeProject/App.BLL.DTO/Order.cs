using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Order : DomainEntityId
{
    [Display(ResourceType = typeof(App.Recources.App.Domain.Order), Name = nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; }
    
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public Guid DeliveryTypeId { get; set; }
    public DeliveryType? DeliveryType { get; set; }
    
    public Guid ShippingInfoId { get; set; }
    
    public Guid PaymentTypeId { get; set; }
    public PaymentType? PaymentType { get; set; }

    public TransactionReport? TransactionReport { get; set; }
    public ICollection<ProductOrders>? ProductOrders { get; set; }
}