using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Base.Domain;

namespace App.Public.DTO.v1;

public class DeliveryType : DomainEntityId
{
    [MaxLength(256)]
    public string TypeName { get; set; } = default!;

    [MaxLength(256)]
    public string? Comment { get; set; }

    public int Price { get; set; }

    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
    public ICollection<Order>? Orders { get; set; }
}