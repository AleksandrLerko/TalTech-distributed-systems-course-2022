using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;


namespace App.DAL.DTO;

public class DeliveryType : DomainEntityId
{
    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.DeliveryType), Name = nameof(TypeName))]
    public LangStr TypeName { get; set; } = new();

    [MaxLength(256)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.DeliveryType), Name = nameof(Comment))]
    public string? Comment { get; set; }
    
    public int Price { get; set; }

    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
    public ICollection<App.DAL.DTO.Order>? Orders { get; set; }
}