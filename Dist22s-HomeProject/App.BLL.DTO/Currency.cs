using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class Currency : DomainEntityId
{
    [MaxLength(64)]
    [Column(TypeName = "jsonb")]
    public LangStr CurrencyName { get; set; } = new();

    public ICollection<Product>? Products { get; set; }
}