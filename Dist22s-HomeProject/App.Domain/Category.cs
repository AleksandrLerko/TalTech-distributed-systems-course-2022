using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Category : DomainEntityMetaId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    public LangStr CategoryName { get; set; } = new();

    public ICollection<Product>? Products { get; set; }
    public ICollection<CategoryType>? CategoryTypes { get; set; }
}