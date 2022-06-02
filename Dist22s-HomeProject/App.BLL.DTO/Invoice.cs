using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class Invoice : DomainEntityMetaId
{
    [MaxLength(512)]
    public string Date { get; set; } = default!;

    [MaxLength(64)]
    public string FirstName { get; set; } = default!;
    [MaxLength(64)]
    public string LastName { get; set; } = default!;
    [MaxLength(128)]
    public string Email { get; set; } = default!;
    [MaxLength(64)]
    public string PaymentMethodName { get; set; } = default!;
    [MaxLength(64)]
    public string DeliveryMethodName { get; set; } = default!;
    [MaxLength(256)]
    public string FullAddress { get; set; } = default!;

    public int FinalPrice { get; set; }

    public TransactionReport? TransactionReport { get; set; }
}