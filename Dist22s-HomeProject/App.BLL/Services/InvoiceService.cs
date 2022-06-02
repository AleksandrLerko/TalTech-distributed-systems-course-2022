using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class InvoiceService : BaseEntityService<App.BLL.DTO.Invoice, App.DAL.DTO.Invoice, IInvoiceRepository>, IInvoiceService
{
    public InvoiceService(IInvoiceRepository repository, IMapper<Invoice, DAL.DTO.Invoice> mapper) : base(repository, mapper)
    {
    }
}