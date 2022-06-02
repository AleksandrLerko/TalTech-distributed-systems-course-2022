using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Category : DomainEntityId
{
    [MaxLength(128)]
    public string CategoryName { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }
    public ICollection<CategoryType>? CategoryTypes { get; set; }
}