using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Specification : DomainEntityId
{
    [MaxLength(256)]
    public string SpecificationName { get; set; } = default!;
    
    public Guid ProductId { get; set; }
    
    // public Product? Product { get; set; }

    public ICollection<SpecificationType>? SpecificationTypes { get; set; }
}