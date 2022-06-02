using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Feedback : DomainEntityMetaId
{
    [MaxLength(2048)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Feedback), Name = nameof(Value))]
    public string Value { get; set; } = default!;

    public string TimeWhenPosted { get; set; } = DateTime.Now.ToShortDateString();
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}