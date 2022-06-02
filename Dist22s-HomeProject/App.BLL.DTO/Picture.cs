using Base.Domain;

namespace App.BLL.DTO;

public class Picture : DomainEntityId
{
    public string FilePath { get; set; } = default!;

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
}