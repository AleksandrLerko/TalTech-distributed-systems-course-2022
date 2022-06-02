using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class Seller : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Seller), Name = nameof(SellerName))]
    public string SellerName { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }
}