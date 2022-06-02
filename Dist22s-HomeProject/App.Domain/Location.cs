using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Location : DomainEntityMetaId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Location), Name = nameof(LocationName))]
    public LangStr LocationName { get; set; } = new();

    public ICollection<InStock>? InStocks { get; set; }
}