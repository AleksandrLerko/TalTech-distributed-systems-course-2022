using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Specification : DomainEntityMetaId
{
    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Specification), Name = nameof(SpecificationName))]
    public LangStr SpecificationName { get; set; } = new();
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public ICollection<SpecificationType>? SpecificationTypes { get; set; }
}