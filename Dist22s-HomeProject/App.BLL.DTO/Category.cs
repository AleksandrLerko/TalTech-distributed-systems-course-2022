using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class Category : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Category), Name = nameof(CategoryName))]
    public LangStr CategoryName { get; set; } = new();

    public ICollection<App.BLL.DTO.Product>? Products { get; set; }
    public ICollection<App.BLL.DTO.CategoryType>? CategoryTypes { get; set; }
}