using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class Specification : DomainEntityId
{
    [MaxLength(256)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Specification), Name = nameof(SpecificationName))]
    public LangStr SpecificationName { get; set; } = new();
    public Guid ProductId { get; set; }
    public App.BLL.DTO.Product? Product { get; set; }

    public ICollection<App.BLL.DTO.SpecificationType>? SpecificationTypes { get; set; }
}