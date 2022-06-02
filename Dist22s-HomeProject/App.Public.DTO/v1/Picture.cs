using Base.Domain;

namespace App.Public.DTO.v1;

public class Picture : DomainEntityId
{
    public string FilePath { get; set; } = default!;

    public Guid? ProductId { get; set; }
    // public Product? Product { get; set; }
}