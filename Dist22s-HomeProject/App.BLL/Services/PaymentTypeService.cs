using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class PaymentTypeService : BaseEntityService<App.BLL.DTO.PaymentType, App.DAL.DTO.PaymentType, IPaymentTypeRepository>, IPaymentTypeService
{
    public PaymentTypeService(IPaymentTypeRepository repository, IMapper<PaymentType, DAL.DTO.PaymentType> mapper) : base(repository, mapper)
    {
    }
}