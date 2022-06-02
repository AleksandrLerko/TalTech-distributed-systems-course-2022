using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Order : DomainEntityMetaId
{
    [Display(ResourceType = typeof(App.Recources.App.Domain.Order), Name = nameof(CreatedAt))]
    public new DateTime CreatedAt { get; set; }
    
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