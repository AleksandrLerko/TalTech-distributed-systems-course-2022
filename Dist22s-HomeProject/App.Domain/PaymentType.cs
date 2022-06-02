using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class PaymentType : DomainEntityMetaId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.PaymentType), Name = nameof(TypeName))]
    public LangStr TypeName { get; set; } = new();

    [MaxLength(2046)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.PaymentType), Name = nameof(Comment))]
    public string? Comment { get; set; }

    public ICollection<Order>? Orders { get; set; }
}