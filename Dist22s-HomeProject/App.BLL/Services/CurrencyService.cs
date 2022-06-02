using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class CurrencyService : BaseEntityService<App.BLL.DTO.Currency, App.DAL.DTO.Currency, ICurrencyRepository>, ICurrencyService
{
    public CurrencyService(ICurrencyRepository repository, IMapper<Currency, DAL.DTO.Currency> mapper) : base(repository, mapper)
    {
    }
}