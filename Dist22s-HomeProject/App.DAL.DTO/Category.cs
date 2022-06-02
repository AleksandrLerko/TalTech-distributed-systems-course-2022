using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;


namespace App.DAL.DTO;

public class Category : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Category), Name = nameof(CategoryName))]
    public LangStr CategoryName { get; set; } = new();

    public ICollection<Product>? Products { get; set; }
    public ICollection<CategoryType>? CategoryTypes { get; set; }
}