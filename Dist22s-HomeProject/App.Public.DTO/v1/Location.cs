using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Location : DomainEntityId
{
    [MaxLength(128)]
    public string LocationName { get; set; } = default!;

    public ICollection<InStock>? InStocks { get; set; }
}