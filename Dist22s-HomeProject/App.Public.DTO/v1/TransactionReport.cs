using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class TransactionReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Display(ResourceType = typeof(App.Recources.App.Domain.TransactionReport), Name = nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
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
    // public Invoice? Invoice { get; set; }

    public ICollection<ProductOrders>? ProductOrders { get; set; }
}