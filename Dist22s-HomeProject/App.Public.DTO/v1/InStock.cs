using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class InStock : DomainEntityId
{
    [Display(ResourceType = typeof(App.Recources.App.Domain.InStock), Name = nameof(Quantity))]
    public int Quantity { get; set; }

    public Guid ProductId { get; set; }
    // public Product? Product { get; set; }

    // public Guid LocationId { get; set; }
    // public Location? Location { get; set; }
}