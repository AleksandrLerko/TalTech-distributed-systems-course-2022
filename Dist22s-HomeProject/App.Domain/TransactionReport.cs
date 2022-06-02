using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class TransactionReport : DomainEntityMetaId
{
    [Display(ResourceType = typeof(App.Recources.App.Domain.TransactionReport), Name = nameof(CreatedAt))]
    public new DateTime CreatedAt { get; set; }
    
    [MaxLength(64)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(64)]
    public string LastName { get; set; } = default!;

    [MaxLength(64)]
    public string Email { get; set; } = default!;
    
    public int TotalPrice { get; set; }

    [MaxLength(512)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Recources.App.Domain.TransactionReport), Name = nameof(Comment))]
    public LangStr? Comment { get; set; } = new();

    // public Guid OrderId { get; set; }
    // public App.BLL.DTO.Order? Order { get; set; }

    public Guid InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }

    public ICollection<ProductOrders>? ProductOrders { get; set; }
}