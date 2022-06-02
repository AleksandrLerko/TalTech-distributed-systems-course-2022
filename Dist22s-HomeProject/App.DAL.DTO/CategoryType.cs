using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.DAL.DTO;

public class CategoryType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    public LangStr TypeName { get; set; } = new();
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}