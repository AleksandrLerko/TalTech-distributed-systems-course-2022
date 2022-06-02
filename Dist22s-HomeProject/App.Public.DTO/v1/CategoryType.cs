using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class CategoryType : DomainEntityId
{
    [MaxLength(128)]
    public string TypeName { get; set; } = default!;
    
    public Guid CategoryId { get; set; }
    // public Category? Category { get; set; }
}