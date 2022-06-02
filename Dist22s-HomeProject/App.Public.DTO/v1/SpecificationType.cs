using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class SpecificationType : DomainEntityId
{
    [MaxLength(256)]
    public string TypeName { get; set; } = default!;

    [MaxLength(256)]
    public string TypeValue { get; set; } = default!;

    public Guid SpecificationId { get; set; }
    // public Specification? Specification { get; set; }
}