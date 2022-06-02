using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class SpecificationType : DomainEntityMetaId
{
    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.SpecificationType), Name = nameof(TypeName))]
    public LangStr TypeName { get; set; } = new();

    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.SpecificationType), Name = nameof(TypeValue))]
    public LangStr TypeValue { get; set; } = new();

    public Guid SpecificationId { get; set; }
    public Specification? Specification { get; set; }
}