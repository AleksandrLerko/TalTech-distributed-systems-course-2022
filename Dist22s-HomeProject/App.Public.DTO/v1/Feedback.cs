using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Feedback : DomainEntityId
{
    [MaxLength(2048)]
    [Display(ResourceType = typeof(App.Recources.App.Domain.Feedback), Name = nameof(Value))]
    public string Value { get; set; } = default!;
    
    public string TimeWhenPosted { get; set; } = DateTime.Now.ToShortDateString();
    public Guid? AppUserId { get; set; }

    public Guid ProductId { get; set; }
}