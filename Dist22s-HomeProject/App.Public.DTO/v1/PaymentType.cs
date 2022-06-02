using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class PaymentType : DomainEntityId
{
    [MaxLength(128)]
    public string TypeName { get; set; } = default!;

    [MaxLength(2046)]
    public string? Comment { get; set; }

    public ICollection<Order>? Orders { get; set; }
}