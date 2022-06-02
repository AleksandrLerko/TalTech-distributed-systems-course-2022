using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class CategoryType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.CategoryType), Name = nameof(TypeName))]
    public LangStr TypeName { get; set; } = new();
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}