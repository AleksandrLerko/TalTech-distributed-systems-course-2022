using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Currency : DomainEntityMetaId
{
    [MaxLength(64)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Currency), Name = nameof(CurrencyName))]
    public LangStr CurrencyName { get; set; } = new();

    public ICollection<Product>? Products { get; set; }
}