using Base.Domain;

namespace App.DAL.DTO;

public class ProductOrders : DomainEntityMetaId
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    
    public Guid TransactionReportId { get; set; }
    public TransactionReport? TransactionReport { get; set; }
}