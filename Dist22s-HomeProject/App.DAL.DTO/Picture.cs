using Base.Domain;

namespace App.DAL.DTO;

public class Picture : DomainEntityMetaId
{
    public string FilePath { get; set; } = default!;

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}