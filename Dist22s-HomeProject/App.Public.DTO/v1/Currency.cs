using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;


public class Currency : DomainEntityId
{
    [MaxLength(64)]
    public string CurrencyName { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }
}